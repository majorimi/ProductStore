using System.Collections.Generic;
using ProductStore.Web.Models;

namespace ProductStore.Web.UI.Models.Home
{
	public class HomeModel
	{
		public IEnumerable<ProductCategoryModel> ProductCategories { get; set; }
	}
}