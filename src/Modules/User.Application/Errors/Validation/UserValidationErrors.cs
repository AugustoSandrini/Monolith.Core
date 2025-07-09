using Core.Shared.Errors;

namespace User.Application.Errors.Validation
{
    /// <summary>
    /// Contains the User validation errors.
    /// </summary>
    internal static class UserValidationErrors
    {
        internal static Error UserIdIsRequired => new("User.UserIdIsRequired", "O campo UserId é obrigatório.");
        internal static Error BodyIsRequired => new("User.BodyIsRequired", "O campo Body é obrigatório.");
        internal static Error CpfIsRequired => new("User.CpfIsRequired", "O campo CPF é obrigatório.");
        internal static Error StatusIsRequired => new("User.StatusIsRequired", "O campo Status é obrigatório.");
        internal static Error NameIsRequired => new("User.NameIsRequired", "O campo Nome é obrigatório.");
        internal static Error EmailIsRequired => new("User.EmailIsRequired", "O campo Email é obrigatório.");
        internal static Error PasswordIsRequired => new("User.PasswordIsRequired", "O campo Senha é obrigatório.");
        internal static Error DateOfBirthIsRequired => new("User.DateOfBirthIsRequired", "A Data de Nascimento é obrigatória.");
        internal static Error AddressIsRequired => new("User.AddressIsRequired", "O campo Endereço é obrigatório.");
        internal static Error PhoneIsRequired => new("User.PhoneIsRequired", "O campo Telefone é obrigatório.");
        internal static Error DocumentIsRequired => new("User.DocumentIsRequired", "O campo Documento é obrigatório.");
        internal static Error DocumentIsInvalid => new("User.DocumentIsInvalid", "O CPF deve ser um documento válido.");
        internal static Error PhoneIsInvalid => new("User.PhoneIsInvalid", "O phone deve ser um número válido.");
    }

}
