using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="ValidateUserDocumentPhoneCommandValidator"/> validator.
    /// </summary>
    internal sealed class ValidateUserDocumentPhoneCommandValidator : BaseValidator<ValidateUserDocumentPhoneCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserDocumentPhoneCommandValidator"/> class.
        /// </summary>
        public ValidateUserDocumentPhoneCommandValidator()
        {
            RuleFor(command => command.EstablishmentId)
                .NotNull().WithError(UserValidationErrors.EstablishmentIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.EstablishmentIdIsRequired);

            RuleFor(command => command.Phone)
                .NotNull().WithError(UserValidationErrors.PhoneIsRequired)
                .NotEmpty().WithError(UserValidationErrors.PhoneIsRequired)
                .Must(BeAValidPhone).WithError(UserValidationErrors.PhoneIsInvalid);

            RuleFor(command => command.Document)
                .NotNull().WithError(UserValidationErrors.DocumentIsRequired)
                .NotEmpty().WithError(UserValidationErrors.DocumentIsRequired)
                .Must(BeAValidCpf).WithError(UserValidationErrors.DocumentIsInvalid);

            RuleFor(command => command.RequestedAmount)
                .NotNull().WithError(UserValidationErrors.RequestedAmountIsRequired)
                .NotEmpty().WithError(UserValidationErrors.RequestedAmountIsRequired);
        }
    }
}
