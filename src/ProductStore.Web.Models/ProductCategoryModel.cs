using System;

namespace ProductStore.Web.Models
{
	public class ProductCategoryModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedAtUtc { get; set; }
	}
}