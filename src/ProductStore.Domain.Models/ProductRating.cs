using System;

namespace ProductStore.Domain.Models
{
	public class ProductRating
	{
		public int Id { get; set; }

		public double Rating { get; set; }

		public int CustomerId { get; set; }

		public DateTime CreatedAtUtc { get; set; }

		public Product Product { get; set; }
	}
}