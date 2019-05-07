using System;

namespace ProductStore.Web.Shared.Logging
{
    public interface ILogger
    {
        void Error(Exception ex);
        void Error(string message, Exception ex);
        void Error(string message);
        void Warn(string message);
        void Info(string message);
    }
}
