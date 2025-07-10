using Microsoft.AspNetCore.Mvc;

namespace WebBff.Endpoints.Users.Requests
{
    /// <summary>
    /// Represents the request for updateProfile User.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    /// <param name="Name">The Name.</param>
    /// <param name="Email">The Email.</param>
    /// <param name="DateOfBirth">The DateOfBirth.</param>
    public sealed class UpdateUserRequest
    {
        [FromBody]
        public UpdateProfileContent Content
        {
            get; set;
        }
    }

    public record UpdateProfileContent(string Document, string Name, string Email, DateTimeOffset DateOfBirth, string Password)
    {
    }
}
