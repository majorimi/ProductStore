using System;

namespace ProductStore.Web.UI.Infrastructure
{
	public class ApiResult<T>
	{
		public T Data { get; }

		public string ErrorMessage { get; }

		public int StatusCode { get; }

		public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

		private ApiResult(T data, int statusCode, string errorMessage = null)
		{
			Data = data;
			StatusCode = statusCode;
			ErrorMessage = errorMessage;
		}

		public static ApiResult<T> CreateFailed(int statusCode, string errorMessage)
		{
			if (statusCode < 300)
			{
				throw  new ArgumentException("StatusCode cannot be smaller than 300");
			}

			return new ApiResult<T>(default(T), statusCode, errorMessage);
		}

		public static ApiResult<T> CreateSuccess(int statusCode, T data)
		{
			if (statusCode > 299)
			{
				throw new ArgumentException("StatusCode cannot be grater than 299");
			}
			return new ApiResult<T>(data, statusCode);
		}
	}
}