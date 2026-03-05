using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Repositories;

namespace EStoreFrontEnd.Models.Services
{
	public class CartService
	{
		private readonly ICartRepository _repository;

		public CartService(ICartRepository repository)
		{
			_repository = repository;
		}

		public CartDto LoadByAccount(string memberAccount)
		{
			//如果此會員沒有購物車，則立即新增一筆

			var cart = _repository.LoadByMember(memberAccount);
			if (cart == null)
			{
				_repository.CreateCart(memberAccount);
				cart = _repository.LoadByMember(memberAccount);
			}
			return cart;
		}


		/// <summary>
		/// 加入一項商品到購物車中，如果此商品已經存在於購物車中，則數量加1
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		/// <exception cref="NotImplementedException"></exception>
		public void IncrementCartItem(int cartId,int productId)
		{
			var cart = _repository.Load(cartId);
			var cartItem = cart.Items
				.FirstOrDefault(x => x.ProductId == productId);
			if (cartItem == null)
			{
				_repository.AddCartItem(cartId, productId);
			}
			else
			{
				_repository.UpdateCartItem(cartId, productId, cartItem.Quantity + 1);
			}
		}

		/// <summary>
		/// 將購物車中的某項商品數量減1
		/// 如果此商品的數量已經是1，則從購物車中移除此項商品
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		/// <exception cref="NotImplementedException"></exception>
		public void DecrementCartItem(int cartId, int productId)
		{
			var cart = _repository.Load(cartId);
			var cartItem = cart.Items
				.FirstOrDefault(x => x.ProductId == productId);
			if (cartItem == null) return;
			if(cartItem.Quantity == 1)
			{
				_repository.RemoveCartItem(cartId, productId);
			}
			else
			{
				_repository.UpdateCartItem(cartId, productId, cartItem.Quantity - 1);

			};
		}

		/// <summary>
		/// 刪除會員的購物車資料，在結帳完成後呼叫此方法來清空購物車資料
		/// </summary>
		/// <param name="memberaccount"></param>
		/// <exception cref="NotImplementedException"></exception>
		public void ClearCart(string memberAccount)
		{
			_repository.DeleteCart(memberAccount);
		}
	}
}
