using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Commands;
using FluentValidation;

namespace User.Application.UseCases.Commands.Validators
{
    /// <summary>
    /// Represents the <see cref="UpdateCreditConsultationStatusCommandValidator"/> validator.
    /// </summary>
    internal sealed class UpdateCreditConsultationStatusCommandValidator : BaseValidator<UpdateCreditConsultationStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCreditConsultationStatusCommandValidator"/> class.
        /// </summary>
        public UpdateCreditConsultationStatusCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);

            RuleFor(command => command.OrderId)
                .NotNull().WithError(UserValidationErrors.OrderIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.OrderIdIsRequired);

            RuleFor(command => command.DecisionIp)
                .NotNull().WithError(UserValidationErrors.DecisionIpIsRequired)
                .NotEmpty().WithError(UserValidationErrors.DecisionIpIsRequired);

            RuleFor(command => command.Status)
                .NotNull().WithError(UserValidationErrors.StatusIsRequired)
                .NotEmpty().WithError(UserValidationErrors.StatusIsRequired);
        }
    }
}
