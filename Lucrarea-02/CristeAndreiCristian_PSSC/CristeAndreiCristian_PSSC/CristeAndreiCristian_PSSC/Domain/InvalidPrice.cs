using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    [Serializable]
    internal class InvalidPrice : Exception
    {
        public InvalidPrice()
        {
        }

        public InvalidPrice(string? message) : base(message)
        {
        }

        public InvalidPrice(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPrice(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
