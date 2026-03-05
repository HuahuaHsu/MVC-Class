using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStoreFrontEnd.Controllers
{
	public class CartController : Controller
	{
		private readonly CartService _service;

		public CartController(CartService service)
		{
			this._service = service;
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

			return View(cartVm);
		}
	}
}