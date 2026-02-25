using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebApplication1.Models.ViewModels
{
	public class ProductIndexViewModel
	{
		public List<ProductIndexItemViewModel> Data {  get; set; }
		public ProductCriteria Criteria { get; set; }
	}


	public class ProductIndexItemViewModel
	{
		public int Id { get; set; }

		[Display(Name="分類名稱")]
		public string CategoryName { get; set; }

		[Display(Name = "商品名稱")]
		public string ProductName { get; set; }

		[Display(Name = "售價")]
		public decimal UnitPrice { get; set; }
	}
}
