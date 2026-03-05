using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;
using Microsoft.EntityFrameworkCore;

namespace EStoreFrontEnd.Models.Repositories
{
	public interface ICartRepository
	{
		void AddCartItem(int cartId, int productId, int qty = 1);
		void CreateCart(string memberAccount);
		void DeleteCart(string memberAccount);
		CartDto Load(int cartId);
		CartDto LoadByMember(string memberAccount);
		void RemoveCartItem(int cartId, int productId);
		void UpdateCartItem(int cartId, int productId, int newQty);
	}

	public class CartRepository : ICartRepository
	{
		private readonly EStoreContext _context;

		public CartRepository(EStoreContext context)
		{
			_context = context;
		}

		public CartDto Load(int cartId)
		{
			var cart = _context.Carts
				.AsNoTracking()
				.Include(c => c.CartItems)
				.ThenInclude(ci => ci.Product)
				.Where(c => c.Id == cartId)
				.ToDto()
				.FirstOrDefault();
			return cart;
		}
		/// <summary>
		/// 載入會員的購物車資料，如果沒有則新增一筆後再回傳
		/// </summary>
		/// <param name="memberAccount"></param>
		/// <return></return>
		public CartDto LoadByMember(string memberAccount)
		{
			var cart = _context.Carts
				.AsNoTracking()
				.Include(c => c.CartItems)
				.ThenInclude(ci => ci.Product)
				.Where(c => c.MemberAccount == memberAccount)
				.ToDto()
				.FirstOrDefault();
			return cart;
		}

		/// <summary>
		/// 新增一筆購物車資料，並回傳新增後的資料
		/// </summary>
		/// <param name="memberAccount"></param>
		public void CreateCart(string memberAccount)
		{
			var cart = new Cart
			{
				MemberAccount = memberAccount
			};
			_context.Carts.Add(cart);
			_context.SaveChanges();
		}

		/// <summary>
		/// 刪除會員的購物車資料，在結帳完成後會呼叫此方法來清空購物車資料
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		public void DeleteCart(string memberAccount)
		{
			var cart = _context.Carts.FirstOrDefault(c => c.MemberAccount == memberAccount);
			if (cart == null) return;
			_context.Carts.Remove(cart);
			_context.SaveChanges();
		}


		/// <summary>
		/// 加入一個商品到會員的購物車中
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		/// <param name="qty"></param>
		public void AddCartItem(int cartId, int productId, int qty = 1)
		{
			var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == cartId);
			if (cart == null) return;

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					ProductId = productId,
					Qty = qty
				};
				cart.CartItems.Add(cartItem);
			}
			else
			{
				cartItem.Qty += qty;
			}
			_context.SaveChanges();
		}

		/// <summary>
		/// 從會員的購物車中移除一個商品
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		public void RemoveCartItem(int cartId, int productId)
		{
			var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == cartId);
			if (cart == null) return;

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem == null) return;

			cart.CartItems.Remove(cartItem);

			_context.SaveChanges();
		}

		/// <summary>
		/// 更新會員購物車中某個商品的數量
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		/// <param name="newQty"></param>
		public void UpdateCartItem(int cartId, int productId, int newQty)
		{
			var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == cartId);
			if (cart == null) return;

			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					ProductId = productId,
					Qty = newQty
				};
				cart.CartItems.Add(cartItem);
			}
			else
			{
				cartItem.Qty = newQty;
			}
			_context.SaveChanges();
		}

	}
}
