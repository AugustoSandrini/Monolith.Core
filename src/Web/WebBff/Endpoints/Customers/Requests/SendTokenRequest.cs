namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for send sms token.
    /// </summary>
    /// <param name="Cpf">The Cpf.</param>
    public record SendTokenRequest(string Cpf)
    {
    }
}
