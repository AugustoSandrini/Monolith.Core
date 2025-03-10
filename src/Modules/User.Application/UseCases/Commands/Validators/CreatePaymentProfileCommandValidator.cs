using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="CreatePaymentProfileCommandValidator"/> validator.
    /// </summary>
    internal sealed class CreatePaymentProfileCommandValidator : BaseValidator<CreatePaymentProfileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePaymentProfileCommandValidator"/> class.
        /// </summary>
        public CreatePaymentProfileCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);

            RuleFor(command => command.GatewayToken)
                .NotNull().WithError(UserValidationErrors.GatewayTokenIsRequired)
                .NotEmpty().WithError(UserValidationErrors.GatewayTokenIsRequired);
        }
    }
}
