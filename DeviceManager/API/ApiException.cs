using System;

namespace MartinGC94.DeviceManager.API
{
    [Serializable]
    public sealed class ApiException : Exception
    {
        public ApiException() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}