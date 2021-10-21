using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
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
