using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Domain.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Desctiption { get; set; }
		
		public byte[] Image { get; set; }
	}
}