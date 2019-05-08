using AutoMapper;
using ProductStore.Domain.Models;
using ProductStore.Web.Models;

namespace ProductStore.Web.Api
{
	public static class MapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize((config) =>
			{
				config.CreateMap<ProductCategory, ProductCategoryModel>();

				config.CreateMap<Product, ProductModel>()
					.ForMember(m => m.CategoryId, opt => opt.MapFrom(s => s.ProductCategory.Id))
					.ForMember(m => m.Rating, opt => opt.MapFrom(s => s.GetRating()));

			});

			Mapper.AssertConfigurationIsValid();
		}
	}
}