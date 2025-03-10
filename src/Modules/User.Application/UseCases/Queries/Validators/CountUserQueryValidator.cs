using User.Application.Errors.Validation;
using User.Shared.Queries;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="CountUserQueryValidator"/> validator.
    /// </summary>
    internal sealed class CountUserQueryValidator : BaseValidator<CountUserQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountUserQueryValidator"/> class.
        /// </summary>
        public CountUserQueryValidator()
        {
        }
    }
}
