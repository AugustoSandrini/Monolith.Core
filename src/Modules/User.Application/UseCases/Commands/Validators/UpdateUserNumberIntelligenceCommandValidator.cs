using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="UpdateUserNumberIntelligenceCommandValidator"/> validator.
    /// </summary>
    internal class UpdateUserNumberIntelligenceCommandValidator : BaseValidator<UpdateUserNumberIntelligenceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserNumberIntelligenceCommandValidator"/> class.
        /// </summary>
        public UpdateUserNumberIntelligenceCommandValidator()
        {
            RuleFor(command => command.Token)
                .NotNull().WithError(UserValidationErrors.TokenIsRequired)
                .NotEmpty().WithError(UserValidationErrors.TokenIsRequired);
        }
    }
}
