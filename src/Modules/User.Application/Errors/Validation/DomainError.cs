using Core.Shared.Errors;

namespace User.Application.Errors.Validation
{
    internal static class DomainError
    {
        internal static Error EmailNotRegistered => new("Email.EmailNotRegistered", "Email não registrado.");

        internal static Error EmailAlreadyAdded => new("Email.EmailAlreadyAdded", "Este endereço de e-mail está associado a outro CPF, por isso não é possível continuar.");

        internal static Error UserNotFound => new("User.UserNotFound", "Cliente não encontrado.");

        internal static Error PhoneNotFound => new("Phone.PhoneNotFound", "Telefone não encontrado para o cliente informado.");

        internal static Error EmailNotFound => new("Phone.EmailNotFound", "Email não encontrado para o cliente informado.");

        internal static Error AlreadyRegisteredNumber => new("User.AlreadyRegisteredNumber", "Este número de celular está associado a outro CPF, por isso não é possível realizar a consulta. Verifique com o cliente o celular cadastrado anteriormente para consultar o crédito disponível.");

        internal static Error AlreadyRegisteredDocument => new("User.AlreadyRegisteredDocument", "Existe outro número de celular associado a este CPF, por isso não é possível realizar a consulta. Verifique com o cliente o celular cadastrado anteriormente para consultar o crédito disponível.");

        internal static Error UserBlocked => new("User.UserBlocked", "Houve um problema com o CPF consultado e por isso não podemos seguir.");
        
        internal static Error UserStatusNowAllowed => new("User.UserStatusNowAllowed", "Status do cliente não permite seguir com atualização de perfil.");

        internal static Error DocumentIsNotAdded => new("User.DocumentIsNotAdded", "Não existe uma conta para esse CPF.");

        internal static Error InvalidPhoneNumber => new("Phone.InvalidPhoneNumber", "Número de telefone inválido.");

        internal static Error Auth0IncludingUserFailed => new("User.Auth0IncludingUserFailed", "Falha ao atualizar os dados do perfil. Por favor, tente novamente mais tarde");
    }
}
