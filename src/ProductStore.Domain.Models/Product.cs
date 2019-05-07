using System;
using System.Collections.Generic;

namespace ProductStore.Domain.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public bool Deleted { get; set; }

		public DateTime CreatedAtUtc { get; set; }

		public ProductCategory ProductCategory { get; set; }
		public ICollection<ProductRating> ProductRatings { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; }

		public Product()
		{
			ProductRatings = new List<ProductRating>();
			ProductImages = new List<ProductImage>();
		}
	}
}