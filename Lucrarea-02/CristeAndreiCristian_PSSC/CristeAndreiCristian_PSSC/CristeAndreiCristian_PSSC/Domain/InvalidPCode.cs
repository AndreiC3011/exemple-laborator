using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    [Serializable]
    internal class InvalidProductCode : Exception
    {
        public InvalidProductCode()
        {
        }

        public InvalidProductCode(string? message) : base(message)
        {
        }

        public InvalidProductCode(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidProductCode(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
