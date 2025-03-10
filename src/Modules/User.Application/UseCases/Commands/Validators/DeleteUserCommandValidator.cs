using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="DeleteUserCommandValidator"/> validator.
    /// </summary>
    internal sealed class DeleteUserCommandValidator : BaseValidator<DeleteUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommandValidator"/> class.
        /// </summary>
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
