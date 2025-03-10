using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetUserPhoneQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetUserPhoneQueryValidator : BaseValidator<GetUserPhoneQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserPhoneQueryValidator"/> class.
        /// </summary>
        public GetUserPhoneQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
