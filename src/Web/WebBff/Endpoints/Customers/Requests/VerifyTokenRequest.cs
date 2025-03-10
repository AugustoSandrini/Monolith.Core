namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for verifyToken.
    /// </summary>
    /// <param name="Cpf">The Cpf.</param>
    /// <param name="Code">The Code.</param>
    public record VerifyTokenRequest(string Cpf, string Code)
    {
    }
}
