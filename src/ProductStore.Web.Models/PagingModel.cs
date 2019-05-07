using System;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Web.Models
{
	public class PagingModel
	{
		[Required()]
		[Range(1, 100)]
		public int  PageSize { get; set; }

		[Required()]
		[Range(1, int.MaxValue)]
		public int PageIndex { get; set; }
	}
}