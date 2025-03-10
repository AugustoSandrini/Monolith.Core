namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for Auth0 send sms token.
    /// </summary>
    /// <param name="To">The To.</param>
    /// <param name="Body">The Body.</param>
    public record SendAuth0TokenRequest(string To, string Body)
    {
    }
}
