using System;
using ProductStore.Web.Api.Infrastructure.Extensions;

namespace ProductStore.Web.Api.Infrastructure.Filters.Swagger
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class StreamPayloadAttribute : Attribute
	{
		public string Name { get; }

		public string In { get; }

		public string Description { get; }

		public string Type { get; }

		public string MediaType { get; }

		public bool Required { get; }

		public StreamPayloadAttribute()
		{
			Name = "File";
			In = "formData";
			Description = "Upload file";
			MediaType = HttpResponseExtensions.StreamMediaTypeValue;
			Type = "file";
			Required = true;
		}
	}
}