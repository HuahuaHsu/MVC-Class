using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EStoreFrontEnd.Models.Repositories
{
	public interface IProductRepository
	{
		List<ProductDto> GetAll();
	}

	public class ProductRepository : IProductRepository
	{
		private readonly EStoreContext _context;

		public ProductRepository(EStoreContext context)
		{
			_context = context;
		}

		public List<ProductDto> GetAll()
		{
			var data = _context.Products
				.AsNoTracking()
				.Include(p => p.Category)
				.OrderBy(p => p.Category.DisplayOrder)
				.ToDto()
				.ToList();

			return data;
		}
	}
}
