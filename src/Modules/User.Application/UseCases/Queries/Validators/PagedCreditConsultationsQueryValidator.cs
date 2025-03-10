using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="PagedCreditConsultationsQueryValidator"/> validator.
    /// </summary>
    internal sealed class PagedCreditConsultationsQueryValidator : BaseValidator<PagedCreditConsultationsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCreditConsultationsQueryValidator"/> class.
        /// </summary>
        public PagedCreditConsultationsQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);

            RuleFor(command => command.Paging)
                .NotNull().WithError(UserValidationErrors.PagingIsRequired)
                .NotEmpty().WithError(UserValidationErrors.PagingIsRequired);
        }
    }
}
