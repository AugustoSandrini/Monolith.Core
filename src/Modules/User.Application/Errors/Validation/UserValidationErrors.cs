using Core.Shared.Errors;

namespace User.Application.Errors.Validation
{
    /// <summary>
    /// Contains the User validation errors.
    /// </summary>
    internal static class UserValidationErrors
    {
        /// <summary>
        /// Gets the UserId is required error.
        /// </summary>
        internal static Error UserIdIsRequired => new("User.UserIdIsRequired", "O campo UserId é obrigatório.");

        /// <summary>
        /// Gets the PaymentProfileId is required error.
        /// </summary>
        internal static Error PaymentProfileIdIsRequired => new("User.PaymentProfileIdRequired", "O campo PaymentProfileId é obrigatório.");

        /// <summary>
        /// Gets the Auth0ExternalUserId is required error.
        /// </summary>
        internal static Error Auth0ExternalUserIdIsRequired => new("User.Auth0ExternalUserIdIsRequired", "O campo Auth0ExternalUserId é obrigatório.");

        /// <summary>
        /// Gets the GatewayToken is required error.
        /// </summary>
        internal static Error GatewayTokenIsRequired => new("User.GatewayTokenIsRequired", "O campo GatewayToken é obrigatório.");

        /// <summary>
        /// Gets the To is required error.
        /// </summary>
        internal static Error ToIsRequired => new("User.ToIsRequired", "O campo To é obrigatório.");

        /// <summary>
        /// Gets the Body is required error.
        /// </summary>
        internal static Error BodyIsRequired => new("User.BodyIsRequired", "O campo Body é obrigatório.");

        /// <summary>
        /// Gets the Cpf is required error.
        /// </summary>
        internal static Error CpfIsRequired => new("User.CpfIsRequired", "O campo CPF é obrigatório.");

        /// <summary>
        /// Gets the OrderId is required error.
        /// </summary>
        internal static Error OrderIdIsRequired => new("User.OrderIdIsRequired", "O campo OrderId é obrigatório.");

        /// <summary>
        /// Gets the DecisionIp is required error.
        /// </summary>
        internal static Error DecisionIpIsRequired => new("User.DecisionIpIsRequired", "O campo DecisionIp é obrigatório.");

        /// <summary>
        /// Gets the Status is required error.
        /// </summary>
        internal static Error StatusIsRequired => new("User.StatusIsRequired", "O campo Status é obrigatório.");

        /// <summary>
        /// Gets the Token is required error.
        /// </summary>
        internal static Error TokenIsRequired => new("User.TokenIsRequired", "O campo Token é obrigatório.");

        /// <summary>
        /// Gets the IsValid is required error.
        /// </summary>
        internal static Error IsValidIsRequired => new("User.IsValidIsRequired", "O campo IsValid é obrigatório.");

        /// <summary>
        /// Gets the Name is required error.
        /// </summary>
        internal static Error NameIsRequired => new("User.NameIsRequired", "O campo Nome é obrigatório.");

        /// <summary>
        /// Gets the Email is required error.
        /// </summary>
        internal static Error EmailIsRequired => new("User.EmailIsRequired", "O campo Email é obrigatório.");

        /// <summary>
        /// Gets the Password is required error.
        /// </summary>
        internal static Error PasswordIsRequired => new("User.PasswordIsRequired", "O campo Senha é obrigatório.");

        /// <summary>
        /// Gets the DateOfBirth is required error.
        /// </summary>
        internal static Error DateOfBirthIsRequired => new("User.DateOfBirthIsRequired", "A Data de Nascimento é obrigatória.");

        /// <summary>
        /// Gets the Address is required error.
        /// </summary>
        internal static Error AddressIsRequired => new("User.AddressIsRequired", "O campo Endereço é obrigatório.");

        /// <summary>
        /// Gets the EstablishmentId is required error.
        /// </summary>
        internal static Error EstablishmentIdIsRequired => new("User.EstablishmentIdIsRequired", "O campo EstablishmentId é obrigatório.");

        /// <summary>
        /// Gets the Phone is required error.
        /// </summary>
        internal static Error PhoneIsRequired => new("User.PhoneIsRequired", "O campo Telefone é obrigatório.");

        /// <summary>
        /// Gets the Document is required error.
        /// </summary>
        internal static Error DocumentIsRequired => new("User.DocumentIsRequired", "O campo Documento é obrigatório.");

        /// <summary>
        /// Error indicating that the document is invalid.
        /// </summary>
        internal static Error DocumentIsInvalid => new("User.DocumentIsInvalid", "O CPF deve ser um documento válido.");

        /// <summary>
        /// Gets the RequestedAmount is required error.
        /// </summary>
        internal static Error RequestedAmountIsRequired => new("User.RequestedAmountIsRequired", "O campo Valor Solicitado é obrigatório.");

        /// <summary>
        /// Gets the Code is required error.
        /// </summary>
        internal static Error CodeIsRequired => new("User.CodeIsRequired", "O campo Código é obrigatório.");

        /// <summary>
        /// Gets the Paging is required error.
        /// </summary>
        internal static Error PagingIsRequired => new("User.PagingIsRequired", "O campo Paging é obrigatório.");

        /// <summary>
        /// Error indicating that the phone is invalid.
        /// </summary>
        internal static Error PhoneIsInvalid => new("User.PhoneIsInvalid", "O phone deve ser um número válido.");
    }

}
