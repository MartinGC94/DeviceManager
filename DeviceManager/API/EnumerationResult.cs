using System;

namespace MartinGC94.DeviceManager.API
{
    public class EnumerationResult<T>
    {
        /// <summary>
        /// The item we are enumerating for. This will only have a value if <see cref="exception"/> is null.
        /// </summary>
        public T item;

        /// <summary>
        /// An identifier of the item where the error occurred. Typically an index, but can also be a real ID.
        /// </summary>
        public bool Success => exception is null;

        /// <summary>
        /// An identifier of the item where the error occurred. Typically an index, but can also be a real ID.
        /// </summary>
        public object itemIdentifier;
        /// <summary>
        /// The exception that specifies the error that occurred during enumeration. This will only have a value if <see cref="item"/> is null.
        /// </summary>
        public Exception exception;

        internal EnumerationResult(object identifier, Exception errorInfo)
        {
            itemIdentifier = identifier;
            exception = errorInfo;
        }

        internal EnumerationResult(T item)
        {
            this.item = item;
        }
    }
}
