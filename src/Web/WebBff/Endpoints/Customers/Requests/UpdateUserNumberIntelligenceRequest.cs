namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for update UserNumberIntelligence.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    /// <param name="IsValid">The isValid.</param>
    public record UpdateUserNumberIntelligenceRequest(string Token, NiAttributes NiAttributes)
    {
    }

    public record NiAttributes(
            SimSwap SimSwap,
            NationalIdentityNumber NationalIdentityNumber);

    public record SimSwap(
            Error Error = null,
            string SimSwapOccurred = default);
    public record Error(int Id = default, string Name = default, string Description = default);
    public record NationalIdentityNumber(string Match);
}
