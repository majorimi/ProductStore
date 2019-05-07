using System;

namespace ProductStore.Web.Shared.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string query)
			: base(message: $"Entity was not found by {query}")
		{}
	}
}