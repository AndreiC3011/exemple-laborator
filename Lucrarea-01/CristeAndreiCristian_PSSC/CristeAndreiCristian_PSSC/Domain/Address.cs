using System.Text.RegularExpressions;

namespace CristeAndreiCristian_PSSC.Domain
{
    class Address
    {
        private static readonly Regex ValidAdress = new("{str, nr}");

        public string _Address { get; }

        private Address(string address)
        {
            if (ValidAdress.IsMatch(address))
            {
                _Address = address;
            }
            else
            {
                throw new InvalidAdress("");
            }
        }

        public override string ToString()
        {
            return _Address;
        }
    }
}
