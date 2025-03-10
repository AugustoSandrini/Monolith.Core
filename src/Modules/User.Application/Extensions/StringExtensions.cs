using System.Text.RegularExpressions;

namespace User.Application.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpecialCharacters(this string input)
            => Regex.Replace(input, @"[^0-9a-zA-Z\._@+]", string.Empty);

        public static string RemoveNonAlphaNumericCharacters(this string input)
            => Regex.Replace(input, @"[^0-9a-zA-Z_@]", string.Empty);

        public static string FormatCpf(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return string.Empty;

            return cpf.Length == 11 ? $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}.{cpf[9..]}" : cpf;
        }
    }
}
