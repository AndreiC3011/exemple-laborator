using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace CristeAndreiCristian_PSSC_3.Domain
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

        public static Option<Address> TryParse(string addressString)
        {
            if (IsValid(addressString))
            {
                return Some<Address>(new(addressString));
            }
            else
            {
                return None;
            }
        }
    }
}
