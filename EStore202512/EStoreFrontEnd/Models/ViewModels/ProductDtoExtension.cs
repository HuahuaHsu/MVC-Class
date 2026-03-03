using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;

namespace EStoreFrontEnd.Models.ViewModels
{
	public static class ProductDtoExtension
	{
		// 這裡的 ToViewModel 方法將 ProductDto 轉換為 ProductIndexItemViewModel，並且直接將 DTO 的屬性對應到 ViewModel 的屬性
		public static ProductIndexItemViewModel ToViewModel(this ProductDto dto)
		{
			return new ProductIndexItemViewModel
			{
				Id = dto.Id,
				Name = dto.Name,
				CategoryName = dto.CategoryName,
				Price = dto.Price,
				ProductImage = dto.ProductImage
			};
		}

		// 這裡的 ToDto 方法將 Product 實體轉換為 ProductDto，並且使用 Select 來投影每個 Product 實體到 ProductDto
		public static IQueryable<ProductDto> ToDto(this IQueryable<Product> model)
		{
			return model.Select(vm => new ProductDto
			{
				Id = vm.Id,
				Name = vm.Name,
				CategoryName = vm.Category.Name,
				Price = vm.Price,
				ProductImage = vm.ProductImage
			});
		}
	}
}
