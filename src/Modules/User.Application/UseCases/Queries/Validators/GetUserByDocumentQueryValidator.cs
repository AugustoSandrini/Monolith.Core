using Core.Application.Extensions;
using User.Application.Errors.Validation;
using User.Shared.Queries;
using FluentValidation;

namespace User.Application.UseCases.Queries.Validators
{
    /// <summary>
    /// Represents the <see cref="GetUserByDocumentQueryValidator"/> validator.
    /// </summary>
    internal sealed class GetUserByDocumentQueryValidator : BaseValidator<GetUserByDocumentQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByDocumentQueryValidator"/> class.
        /// </summary>
        public GetUserByDocumentQueryValidator()
        {
            RuleFor(command => command.Document)
                .NotNull().WithError(UserValidationErrors.DocumentIsRequired)
                .NotEmpty().WithError(UserValidationErrors.DocumentIsRequired);
        }
    }
}
