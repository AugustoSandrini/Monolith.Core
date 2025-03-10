using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetCreditConsultationByOrderIdQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetCreditConsultationByOrderIdQueryValidator : BaseValidator<GetCreditConsultationByOrderIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCreditConsultationByOrderIdQueryValidator"/> class.
        /// </summary>
        public GetCreditConsultationByOrderIdQueryValidator()
        {
            RuleFor(command => command.OrderId)
                .NotNull().WithError(UserValidationErrors.OrderIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.OrderIdIsRequired);

            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
