using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetUserEmailQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetUserEmailQueryValidator : BaseValidator<GetUserEmailQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserEmailQueryValidator"/> class.
        /// </summary>
        public GetUserEmailQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
