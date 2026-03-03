using EStoreFrontEnd.Models.Services;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EStoreFrontEnd.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ProductService _service;

		public ProductsController(ProductService service)
		{
			_service = service;
		}

		public IActionResult Index()
		{
			var products = _service
				.GetAllForIndex()
				.Select(p => p.ToViewModel())
				.ToList();
			return View(products);
		}
	}
}
