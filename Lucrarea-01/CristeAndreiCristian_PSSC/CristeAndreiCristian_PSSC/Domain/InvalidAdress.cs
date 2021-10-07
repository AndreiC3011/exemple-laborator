using System;
using System.Runtime.Serialization;

namespace CristeAndreiCristian_PSSC.Domain
{
    [Serializable]
    internal class InvalidAdress : Exception
    {
        public InvalidAdress()
        {
        }

        public InvalidAdress(string? message) : base(message)
        {
        }

        public InvalidAdress(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidAdress(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}