using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record Address
    {
        private static readonly Regex ValidPattern = new("^.*$");

        public string _address { get; }

        public Address(string address)
        {
            if (ValidPattern.IsMatch(address))
            {
                _address = address;
            }
            else
            {
                throw new InvalidAddress("");
            }
        }

        public override string ToString()
        {
            return _address;
        }
        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public static bool TryParse(string addressString, out Address address)
        {
            bool isValid = false;
            address = null;
            if (IsValid(addressString))
            {
                isValid = true;
                address = new(addressString);
            }
            return isValid;
        }

    }
}
