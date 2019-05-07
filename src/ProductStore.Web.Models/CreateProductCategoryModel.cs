using System.ComponentModel.DataAnnotations;

namespace ProductStore.Web.Models
{
	public class CreateProductCategoryModel
	{
		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Name { get; set; }
	}
}