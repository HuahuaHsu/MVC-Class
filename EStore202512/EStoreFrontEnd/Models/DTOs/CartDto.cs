using EStoreFrontEnd.Models.EfModels;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EStoreFrontEnd.Models.DTOs
{
	public class CartItemDto
	{
		public int Id { get; set; }

		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public decimal UnitPrice { get; set; }

		public int Quantity { get; set; }

		public string ProductImage { get; set; }

		public decimal SubTotal=> UnitPrice * Quantity;
	}

	public class CartDto
	{
		public int Id { get; set; }
		public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

		public decimal Total => Items.Sum(i => i.SubTotal);
	}

	public static class CartDtoExtensions
	{
		public static IQueryable<CartDto> ToDto(this IQueryable<Cart> query)
		{
			return query.Select(entity => new CartDto
			{
				Id = entity.Id,
				Items = entity.CartItems.Select(i => new CartItemDto
				{
					Id = i.Id,
					ProductId = i.ProductId,
					ProductName = i.Product.Name,
					UnitPrice = i.Product.Price,
					Quantity = i.Qty,
					ProductImage = i.Product.ProductImage
				}).ToList()
			});
		}

		public static CartIndexViewModel ToViewModel(this CartDto dto)
		{
			return new CartIndexViewModel
			{   Id = dto.Id,
				Items = dto.Items.Select(i => new CartItemViewModel
				{
					Id = i.Id,
					ProductId = i.ProductId,
					ProductName = i.ProductName,
					UnitPrice = i.UnitPrice,
					Quantity = i.Quantity,
					ProductImage = i.ProductImage
				}).ToList(),

			};
		}
	}
}
