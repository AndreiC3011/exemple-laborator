using System.Text.RegularExpressions;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record ProductCode
    {
        private static readonly Regex ValidProductCode = new("^1[0-9]{3}$");

        public string ProductC { get; }

        private ProductCode(string pC)
        {
            if (ValidProductCode.IsMatch(pC))
            {
                ProductC = pC;
            }
            else
            {
                throw new InvalidProductCode("");
            }
        }

        public override string ToString()
        {
            return ProductC;
        }
    }
}
