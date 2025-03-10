using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="UpsertAddressCommandValidator"/> validator.
    /// </summary>
    internal sealed class UpsertAddressCommandValidator : BaseValidator<UpsertAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpsertAddressCommandValidator"/> class.
        /// </summary>
        public UpsertAddressCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);

            RuleFor(command => command.Address)
                .NotNull().WithError(UserValidationErrors.AddressIsRequired)
                .NotEmpty().WithError(UserValidationErrors.AddressIsRequired);
        }
    }
}
