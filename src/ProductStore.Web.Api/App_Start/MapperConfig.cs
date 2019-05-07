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
					//.ForMember(m => m., opt => opt.Ignore());
			});

			Mapper.AssertConfigurationIsValid();
		}
	}
}