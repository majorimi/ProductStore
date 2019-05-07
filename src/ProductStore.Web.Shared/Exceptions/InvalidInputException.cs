using System;

namespace ProductStore.Web.Shared.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string exception)
            : base(message: $"Unable to generate label {exception}")
        { }
    }
}
