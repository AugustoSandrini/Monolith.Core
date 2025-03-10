using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="SendTokenCommandValidator"/> validator.
    /// </summary>
    internal sealed class SendTokenCommandValidator : BaseValidator<SendTokenCommand>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SendTokenCommandValidator"/> class.
        /// </summary>\
        public SendTokenCommandValidator()
        {
            RuleFor(command => command.Cpf)
                .NotNull().WithError(UserValidationErrors.CpfIsRequired)
                .NotEmpty().WithError(UserValidationErrors.CpfIsRequired)
                .Must(BeAValidCpf).WithError(UserValidationErrors.DocumentIsInvalid);
        }
    }
}
