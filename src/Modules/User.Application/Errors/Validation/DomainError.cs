using Core.Shared.Errors;

namespace User.Application.Errors.Validation
{
    /// <summary>
    /// Contains domain errors related to Users.
    /// </summary>
    internal static class DomainError
    {
        /// <summary>
        /// Gets the error when the email is not registered.
        /// </summary>
        internal static Error EmailNotRegistered => new("Email.EmailNotRegistered", "Email não registrado.");

        /// <summary>
        /// Gets the error when the email is already associated with another CPF.
        /// </summary>
        internal static Error EmailAlreadyAdded => new("Email.EmailAlreadyAdded", "Este endereço de e-mail está associado a outro CPF, por isso não é possível continuar.");

        /// <summary>
        /// Gets the error when the requested amount is off the boundaries.
        /// </summary>
        internal static Error RequestedAmountInvalid => new("User.RequestedAmountInvalid", "O valor solicitado esta fora dos limites estabelecidos pelo fundo pagador.");

        /// <summary>
        /// Gets the error when the User is not found.
        /// </summary>
        internal static Error UserNotFound => new("User.UserNotFound", "Cliente não encontrado.");

        /// <summary>
        /// Gets the error when the phone is not found for the User.
        /// </summary>
        internal static Error PhoneNotFound => new("Phone.PhoneNotFound", "Telefone não encontrado para o cliente informado.");

        /// <summary>
        /// Gets the error when the email is not found for the User.
        /// </summary>
        internal static Error EmailNotFound => new("Phone.EmailNotFound", "Email não encontrado para o cliente informado.");

        /// <summary>
        /// Gets the error when the payment method is not found.
        /// </summary>
        internal static Error PaymentMethodNotFound => new("PaymentMethod.PaymentMethodNotFound", "Método de pagamento não encontrado.");

        /// <summary>
        /// Gets the error when the payment method could not be created.
        /// </summary>
        internal static Error PaymentMethodCouldntBeCreated => new("PaymentMethod.PaymentMethodCouldntBeCreated", "Método de pagamento não pode ser criado.");

        /// <summary>
        /// Gets the error when the payment method could not be verified.
        /// </summary>
        internal static Error PaymentMethodCouldntBeVerified => new("PaymentMethod.PaymentMethodCouldntBeVerified", "Método de pagamento não pode ser verificado.");

        /// <summary>
        /// Gets the error when the cell phone number is already registered with another CPF.
        /// </summary>
        internal static Error AlreadyRegisteredNumber => new("User.AlreadyRegisteredNumber", "Este número de celular está associado a outro CPF, por isso não é possível realizar a consulta. Verifique com o cliente o celular cadastrado anteriormente para consultar o crédito disponível.");

        /// <summary>
        /// Gets the error when another phone number is already associated with the given CPF.
        /// </summary>
        internal static Error AlreadyRegisteredDocument => new("User.AlreadyRegisteredDocument", "Existe outro número de celular associado a este CPF, por isso não é possível realizar a consulta. Verifique com o cliente o celular cadastrado anteriormente para consultar o crédito disponível.");

        /// <summary>
        /// Gets the error when the User is blocked.
        /// </summary>
        internal static Error UserBlocked => new("User.UserBlocked", "Houve um problema com o CPF consultado e por isso não podemos seguir.");
        
        /// <summary>
        /// Gets the error when the User status now allowed.
        /// </summary>
        internal static Error UserStatusNowAllowed => new("User.UserStatusNowAllowed", "Status do cliente não permite seguir com atualização de perfil.");

        /// <summary>
        /// Gets the error when the order is not found.
        /// </summary>
        internal static Error OrderNotFound => new("Order.OrderNotFound", "Pedido não encontrado.");

        /// <summary>
        /// Gets the error when no account exists for the given CPF.
        /// </summary>
        internal static Error DocumentIsNotAdded => new("User.DocumentIsNotAdded", "Não existe uma conta para esse CPF.");

        /// <summary>
        /// Gets the error when the phone number is invalid.
        /// </summary>
        internal static Error InvalidPhoneNumber => new("Phone.InvalidPhoneNumber", "Número de telefone inválido.");

        /// <summary>
        /// Gets the error when the establishment is not found.
        /// </summary>
        internal static Error EstablishmentNotFound => new("Establishment.EstablishmentNotFound", "Estabelecimento não encontrado.");


        /// <summary>
        /// Gets the error when the establishment chain is not found.
        /// </summary>
        internal static Error EstablishmentChainNotFound => new("Establishment.EstablishmentChainNotFound", "Rede de Estabelecimento não encontrado.");

        /// <summary>
        /// Gets the error when the phone is not linked to the CPF.
        /// </summary>
        internal static Error PhoneNotOwned => new("User.PhoneNotOwned", "O CPF informado não possui vínculo com o telefone.");

        /// <summary>
        /// Gets the error when it was not possible to validate the link with the phone due to a timeout.
        /// </summary>
        internal static Error NumberIntelligenceTimeout => new("Phone.NumberIntelligenceTimeout", "Não foi possível validar o vínculo com o telefone, tente novamente.");

        /// <summary>
        /// Gets the error when it was not possible to find a credit analysis for the given order.
        /// </summary>
        internal static Error CreditConsultationNotFound => new("CreditConsultation.CreditConsultationNotFound", "Não foi possível encontrar uma análise de crédito para o pedido informado.");

        /// <summary>
        /// Gets the error when the provided token is invalid.
        /// </summary>
        internal static Error TokenInvalid => new("Phone.TokenInvalid", "Não foi possível validar o token.");

        /// <summary>
        /// Gets the error when there was a failure in sending the authentication SMS.
        /// </summary>
        internal static Error SmsSendingFailed => new("User.SmsSendingFailed", "Falha ao enviar o SMS de autenticação. Por favor, tente novamente mais tarde.");

        /// <summary>
        /// Gets the error when it was not possible to include the user.
        /// </summary>
        internal static Error Auth0IncludingUserFailed => new("User.Auth0IncludingUserFailed", "Falha ao atualizar os dados do perfil. Por favor, tente novamente mais tarde");

        /// <summary>
        /// Gets the error when the paying fund is not found.
        /// </summary>
        internal static Error PayingFundNotFound => new("Establishment.PayingFundNotFound", "Fundo de pagamento não encontrado.");

        /// <summary>
        /// Gets the error when it was not possible to find the requested data with the provided token.
        /// </summary>
        internal static Error NumberIntelligenceNotFound => new("User.NumberIntelligenceNotFound", "Não foi possível encontrar o dado solicitado com o Token:");
    }
}
