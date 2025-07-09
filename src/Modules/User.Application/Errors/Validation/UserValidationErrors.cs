using Core.Shared.Errors;

namespace User.Application.Errors.Validation
{
    /// <summary>
    /// Contains the User validation errors.
    /// </summary>
    internal static class UserValidationErrors
    {
        internal static Error UserIdIsRequired => new("User.UserIdIsRequired", "O campo UserId é obrigatório.");
        internal static Error NameIsRequired => new("User.NameIsRequired", "O campo Nome é obrigatório.");
        internal static Error EmailIsRequired => new("User.EmailIsRequired", "O campo Email é obrigatório.");
        internal static Error PasswordIsRequired => new("User.PasswordIsRequired", "O campo Senha é obrigatório.");
        internal static Error DateOfBirthIsRequired => new("User.DateOfBirthIsRequired", "A Data de Nascimento é obrigatória.");
        internal static Error AddressIsRequired => new("User.AddressIsRequired", "O campo Endereço é obrigatório.");
        internal static Error DocumentIsRequired => new("User.DocumentIsRequired", "O campo Documento é obrigatório.");
        internal static Error DocumentIsInvalid => new("User.DocumentIsInvalid", "O CPF deve ser um documento válido.");
    }

}
