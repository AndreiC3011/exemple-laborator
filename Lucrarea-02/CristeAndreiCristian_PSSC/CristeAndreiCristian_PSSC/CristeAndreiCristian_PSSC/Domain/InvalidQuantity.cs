using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    [Serializable]
    internal class InvalidQuantity : Exception
    {
        public InvalidQuantity()
        {
        }

        public InvalidQuantity(string? message) : base(message)
        {
        }

        public InvalidQuantity(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidQuantity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
