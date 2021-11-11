using LanguageExt;
using static LanguageExt.Prelude;
using System.Text.RegularExpressions;

namespace CristeAndreiCristian_PSSC_3.Domain
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
                throw new InvalidPCode("");
            }
        }

        public override string ToString()
        {
            return Code;
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public static Option<PCode> TryParse(string productCodeString)
        {
            if (IsValid(productCodeString))
            {
                return Some<PCode>(new(productCodeString));
            }
            else
            {
                return None;
            }
        }
    }
}
