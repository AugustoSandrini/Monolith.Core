using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="ListOrderWithEstablishmentInProgressQueryValidator"/> validator.
    /// </summary>
    internal sealed class ListOrderWithEstablishmentInProgressQueryValidator : BaseValidator<ListOrderWithEstablishmentInProgressQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListOrderWithEstablishmentInProgressQueryValidator"/> class.
        /// </summary>
        public ListOrderWithEstablishmentInProgressQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
