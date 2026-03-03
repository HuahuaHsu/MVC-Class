using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Repositories;

namespace EStoreFrontEnd.Models.Services
{
	public class ProductService
	{
		private readonly IProductRepository _repo;

		public ProductService(IProductRepository repo)
		{
			_repo = repo;
		}

		public List<ProductDto> GetAllForIndex()
		{
			return _repo.GetAll();
		}
	}
}
