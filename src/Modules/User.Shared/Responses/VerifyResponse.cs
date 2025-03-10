namespace User.Shared.Responses
{
    public sealed record VerifyResponse(string AccessToken, int ExpiresIn, string IdToken, string TokenType);
}
