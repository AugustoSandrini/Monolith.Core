using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetUserByIdQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetUserByIdQueryValidator : BaseValidator<GetUserByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQueryValidator"/> class.
        /// </summary>
        public GetUserByIdQueryValidator()
        {
            RuleFor(command => command.UserId)
                .NotNull().WithError(UserValidationErrors.UserIdIsRequired)
                .NotEmpty().WithError(UserValidationErrors.UserIdIsRequired);
        }
    }
}
