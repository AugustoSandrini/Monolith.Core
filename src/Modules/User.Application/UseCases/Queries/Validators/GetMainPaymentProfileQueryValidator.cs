using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetMainPaymentProfileQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetMainPaymentProfileQueryValidator : BaseValidator<GetMainPaymentProfileQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMainPaymentProfileQueryValidator"/> class.
        /// </summary>
        public GetMainPaymentProfileQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
