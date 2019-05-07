using System;
using ProductStore.Web.Api.Infrastructure.Extensions;

namespace ProductStore.Web.Api.Infrastructure.Filters.Swagger
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class StreamResponseAttribute : Attribute
	{
		public string Type { get; }

		public string MediaType { get; }

		public StreamResponseAttribute()
		{
			MediaType = HttpResponseExtensions.StreamMediaTypeValue;
			Type = "file";
		}
	}
}