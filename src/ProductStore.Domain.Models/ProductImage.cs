using System;

namespace ProductStore.Domain.Models
{
	public class ProductImage
	{
		public int Id { get; set; }

		public byte[] Image { get; set; }

		public DateTime CreatedAtUtc { get; set; }

		public Product Product { get; set; }
	}
}