using Microsoft.AspNetCore.Mvc;

namespace EStoreFrontEnd.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Additem(int productId)
		{
			var account = User.Identity.Name;//登入者的帳號
			
			//todo 根據 account 和 productId 將商品加入購物車
			
			return new EmptyResult();
		}
	}
}
