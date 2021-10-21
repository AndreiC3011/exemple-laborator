using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record PCode
    {
        private static readonly Regex ValidPattern = new("^.*$");

        public string Code { get; }

        public PCode(string value)
        {
            if (ValidPattern.IsMatch(value))
            {
                Code = value;
            }
            else
            {
                throw new InvalidProductCode("");
            }
        }

        public override string ToString()
        {
            return Code;
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public static bool TryParse(string productCodeString, out PCode productCode)
        {
            bool isValid = false;
            productCode = null;
            if (IsValid(productCodeString))
            {
                isValid = true;
                productCode = new(productCodeString);
            }
            return isValid;
        }
    }
}
