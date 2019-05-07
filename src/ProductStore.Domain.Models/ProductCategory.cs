using System;
using System.Collections.Generic;

namespace ProductStore.Domain.Models
{
	public class ProductCategory
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedAtUtc { get; set; }

		public ICollection<Product> Products { get; set; }

		public ProductCategory()
		{
			Products = new List<Product>();
		}
	}
}