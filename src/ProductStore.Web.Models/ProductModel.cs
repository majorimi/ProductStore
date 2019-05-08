using System;

namespace ProductStore.Web.Models
{
	public class ProductModel
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public decimal Rating { get; set; }

		public DateTime CreatedAtUtc { get; set; }
	}
}