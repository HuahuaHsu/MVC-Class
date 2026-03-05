namespace EStoreFrontEnd.Models.ViewModels
{
	public class CartItemViewModel
	{
		public int Id { get; set; }

		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public decimal UnitPrice { get; set; }

		public int Quantity { get; set; }

		public string ProductImage { get; set; }
		public decimal Subtotal => UnitPrice * Quantity;
	}

	public class CartIndexViewModel
	{
		public int Id { get; set; }
		public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

		public decimal Total => Items.Sum(i => i.Subtotal);

		public bool AllowCheckout => Items.Count > 0;
	}
}
