using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace EStoreFrontEnd.Models.ViewModels
{
	public class ProductIndexItemViewModel
	{
		public int Id { get; set; }

		[Display(Name="商品名稱")]
		public string Name { get; set; }

		[Display(Name="商品分類")]
		public string CategoryName { get; set; }

		[Display(Name = "價格")]
		[DisplayFormat(ApplyFormatInEditMode =false,DataFormatString ="{0:#,#}")]
		public decimal Price { get; set; }

		public string ProductImage { get; set; }
	}
}
