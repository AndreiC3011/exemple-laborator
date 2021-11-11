using System;
using System.Runtime.Serialization;

namespace CristeAndreiCristian_PSSC_3.Domain
{
    [Serializable]
    internal class InvalidPCode : Exception
    {
        public InvalidPCode()
        {
        }

        public InvalidPCode(string? message) : base(message)
        {
        }

        public InvalidPCode(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPCode(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
