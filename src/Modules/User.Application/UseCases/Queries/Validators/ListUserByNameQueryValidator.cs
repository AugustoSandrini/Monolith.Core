using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="ListUserByNameQueryValidator"/> validator.
    /// </summary>
    internal sealed class ListUserByNameQueryValidator : BaseValidator<ListUserByNameQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListUserByNameQueryValidator"/> class.
        /// </summary>
        public ListUserByNameQueryValidator()
        {
            RuleFor(command => command.Name)
                .NotNull().WithError(UserValidationErrors.NameIsRequired)
                .NotEmpty().WithError(UserValidationErrors.NameIsRequired);
        }
    }
}
