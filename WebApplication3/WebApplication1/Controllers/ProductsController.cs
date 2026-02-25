using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.EfModels;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ISpanDemoContext _context;

		public ProductsController(ISpanDemoContext context)
		{
			_context = context;
		}

		public IActionResult Index(ProductCriteria criteria)
		{
			IQueryable<Product> query = _context.Products
				.AsNoTracking()
				.Include(x => x.Category)
				.OrderBy(x => x.Category.DisplayOrder)
				.ThenBy(x => x.UnitPrice);

			if (criteria.PriceStart.HasValue)
			{
				query = query.Where(x => x.UnitPrice >= criteria.PriceStart.Value);
			}
			if (criteria.PriceEnd.HasValue)
			{
				query = query.Where(x => x.UnitPrice <= criteria.PriceEnd.Value);
			}
			if (string.IsNullOrEmpty(criteria.ProductName) == false)
			{
				query = query.Where(x=>x.ProductName.Contains(criteria.ProductName));
			}

			List<ProductIndexItemViewModel> data = query
				.Select(x => new ProductIndexItemViewModel
				{
					Id = x.Id,
					CategoryName = x.Category.CategoryName,
					ProductName = x.ProductName,
					UnitPrice = x.UnitPrice,
				})
				.ToList();

			var vm = new ProductIndexViewModel
			{
				Data = data,
				Criteria = criteria
			};
			return View(vm);
		}
	}
}
