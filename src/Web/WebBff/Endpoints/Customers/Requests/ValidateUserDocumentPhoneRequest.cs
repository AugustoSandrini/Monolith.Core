namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for ValidateUserDocumentPhone.
    /// </summary>
    /// <param name="Document">The Document.</param>
    /// <param name="Phone">The Phone.</param>
    /// <param name="EstablishmentId">The EstablishmentId.</param>
    /// <param name="RequestedAmount">The RequestedAmount.</param>
    public record ValidateUserDocumentPhoneRequest(string Document, string Phone, Guid EstablishmentId, decimal RequestedAmount)
    {
    }
}
