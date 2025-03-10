using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetPaymentProfileQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetPaymentProfileQueryValidator : BaseValidator<GetPaymentProfileQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentProfileQueryValidator"/> class.
        /// </summary>
        public GetPaymentProfileQueryValidator()
        {
            RuleFor(command => command.PaymentProfileId)
                .NotNull().WithError(UserValidationErrors.PaymentProfileIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.PaymentProfileIdIsRequired);
        }
    }
}
