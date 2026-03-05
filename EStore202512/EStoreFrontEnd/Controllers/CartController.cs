using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;
using EStoreFrontEnd.Models.Services;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStoreFrontEnd.Controllers
{
	public class CartController : Controller
	{
		private readonly CartService _service;
		private readonly EStoreContext _context;

		public CartController(CartService service,EStoreContext context)
		{
			this._service = service;
			_context = context;
		}
		public IActionResult AddItem(int productId)
		{
			var account = User.Identity.Name;  // 取得目前登入的使用者帳號

			// TODO: 根據 account 和 productId 將商品加入購物車
			var cartDto = _service.LoadByAccount(account);
			if (cartDto == null) throw new Exception("找不到購物車");

			_service.IncrementCartItem(cartDto.Id, productId);


			return new EmptyResult();  // 回傳結果
		}
		[Authorize]
		public ActionResult Info()
		{
			var customerAccount = User.Identity.Name;
			var cartDto = _service.LoadByAccount(customerAccount);
			var cartVm = cartDto.ToViewModel();

			return View(cartVm);
		}
		[Authorize]
		public ActionResult IncrementItem(int productId)
		{
			var customerAccount = User.Identity.Name;
			var cartDto = _service.LoadByAccount(customerAccount);
			if (cartDto == null) return new EmptyResult();

			_service.IncrementCartItem(cartDto.Id, productId);
			return new EmptyResult();
		}
		[Authorize]
		public ActionResult DecrementItem(int productId)
		{
			var customerAccount = User.Identity.Name;
			var cartDto = _service.LoadByAccount(customerAccount);
			if (cartDto == null) return new EmptyResult();

			_service.DecrementCartItem(cartDto.Id, productId);
			return new EmptyResult();
		}

		public ActionResult Checkout()
		{
			var customerAccount = User.Identity.Name;
			var cartVm = _service.LoadByAccount(customerAccount)?.ToViewModel();
			if(cartVm.AllowCheckout == false)ViewBag.ErrorMessage = "購物車內沒有商品，無法結帳";

			return View();
		}


		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Checkout(CheckoutViewModel vm)
		{
			//驗證輸入資料
			if (!ModelState.IsValid) return View(vm);

			var customerAccount = User.Identity.Name;
			var cart = _service.LoadByAccount(customerAccount).ToViewModel();

			//檢查購物車內是否有商品
			if (cart.AllowCheckout == false)
			{
				ViewBag.ErrorMessage = "購物車內沒有商品，無法結帳";
				return View(vm);
			}

			//處理結帳邏輯
			ProcessCheckout(customerAccount, vm);
			return View("CheckoutConfirm");
		}

		private void ProcessCheckout(string customerAccount, CheckoutViewModel vm)
		{
			//1.建立訂單
			CreateOrder(customerAccount, vm);

			//2.清空購物車
			EmptyCart(customerAccount);
		}

		private void CreateOrder(string customerAccount, CheckoutViewModel vm)
		{
			//取得會員Id
			var memberId = _context.Members.FirstOrDefault(m => m.Account == customerAccount).Id;

			//取得購物車資訊
			CartDto cart = _service.LoadByAccount(customerAccount);

			//建立訂單主檔
			var order = new Order
			{
				MemberId = memberId,
				Total = (int)cart.Total,
				CreatedTime = DateTime.Now,
				Status = 1,//(int)OrderStatuts.未處理
				RequestRefund = false,
				Receiver = vm.Receiver,
				Address = vm.Address,
				CellPhone = vm.CellPhone
			};

			//新增訂單明細
			foreach (var item in cart.Items)
			{
				order.OrderItems.Add(new OrderItem
				{
					ProductId = item.ProductId,
					ProductName = item.ProductName,
					Price = (int)item.UnitPrice,
					Qty = item.Quantity,
					SubTotal = (int)item.SubTotal
				});
			}

			_context.Orders.Add(order);
			_context.SaveChanges();
		}

		private void EmptyCart(string customerAccount)
		{
			_service.ClearCart(customerAccount);
		}
	}
}