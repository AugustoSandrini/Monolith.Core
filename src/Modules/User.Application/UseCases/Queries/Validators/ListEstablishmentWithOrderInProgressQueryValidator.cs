using Core.Application.Extensions;
using User.Shared.Queries;
using User.Application.Errors.Validation;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="ListEstablishmentWithOrderInProgressQueryValidator"/> validator.
    /// </summary>
    internal sealed class ListEstablishmentWithOrderInProgressQueryValidator : BaseValidator<ListEstablishmentsWithOrdersInProgressQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListEstablishmentWithOrderInProgressQueryValidator"/> class.
        /// </summary>
        public ListEstablishmentWithOrderInProgressQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
