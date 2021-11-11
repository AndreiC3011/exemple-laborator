using System;
using System.Runtime.Serialization;

namespace CristeAndreiCristian_PSSC_3.Domain
{
    [Serializable]
    internal class InvalidAddress : Exception
    {
        public InvalidAddress()
        {
        }

        public InvalidAddress(string? message) : base(message)
        {
        }

        public InvalidAddress(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidAddress(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
