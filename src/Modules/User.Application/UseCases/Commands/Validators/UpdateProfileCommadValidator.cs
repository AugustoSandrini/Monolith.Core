using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="UpdateProfileCommadValidator"/> validator.
    /// </summary>
    internal sealed class UpdateProfileCommadValidator : BaseValidator<UpdateProfileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProfileCommadValidator"/> class.
        /// </summary>
        public UpdateProfileCommadValidator()
        {
            RuleFor(command => command.Document)
                .NotNull().WithError(UserValidationErrors.DocumentIsRequired)
                .NotEmpty().WithError(UserValidationErrors.DocumentIsRequired)
                .Must(BeAValidCpf).WithError(UserValidationErrors.DocumentIsInvalid); ;

            RuleFor(command => command.Name)
                .NotNull().WithError(UserValidationErrors.NameIsRequired)
                .NotEmpty().WithError(UserValidationErrors.NameIsRequired);

            RuleFor(command => command.Email)
                .NotNull().WithError(UserValidationErrors.EmailIsRequired)
                .NotEmpty().WithError(UserValidationErrors.EmailIsRequired);

            RuleFor(command => command.DateOfBirth)
                .NotNull().WithError(UserValidationErrors.DateOfBirthIsRequired)
                .NotEmpty().WithError(UserValidationErrors.DateOfBirthIsRequired);

            RuleFor(command => command.Password)
                .NotNull().WithError(UserValidationErrors.PasswordIsRequired)
                .NotEmpty().WithError(UserValidationErrors.PasswordIsRequired);

            RuleFor(command => command.Token)
                .NotNull().WithError(UserValidationErrors.TokenIsRequired)
                .NotEmpty().WithError(UserValidationErrors.TokenIsRequired);
        }
    }
}
