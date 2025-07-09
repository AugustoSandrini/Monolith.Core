using Core.Shared.Errors;

namespace User.Application.Errors.Validation
{
    internal static class DomainError
    {
        internal static Error EmailAlreadyAdded => new("Email.EmailAlreadyAdded", "Este endereço de e-mail está associado a outro CPF, por isso não é possível continuar.");

        internal static Error UserNotFound => new("User.UserNotFound", "Cliente não encontrado.");

        internal static Error PhoneNotFound => new("Phone.PhoneNotFound", "Telefone não encontrado para o cliente informado.");

        internal static Error EmailNotFound => new("Phone.EmailNotFound", "Email não encontrado para o cliente informado.");
        
        internal static Error UserStatusNowAllowed => new("User.UserStatusNowAllowed", "Status do cliente não permite seguir com atualização de perfil.");
    }
}
