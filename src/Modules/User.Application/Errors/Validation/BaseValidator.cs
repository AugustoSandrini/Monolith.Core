using DocumentValidator;
using FluentValidation;
using System.Text.RegularExpressions;

namespace User.Application.Errors.Validation
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        private const int TimeoutRegexInMilliseconds = 250;
        protected static bool BeAValidPhone(string phone)
        {
            try
            {
                string pattern = @"^\+\d{2} \(\d{2}\) \d{5}-\d{4}$";

                var match = Regex.Match(phone, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(TimeoutRegexInMilliseconds));

                return match.Success;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        protected static bool BeAValidCpf(string cpf)
            => CpfValidation.Validate(cpf);
    }
}
