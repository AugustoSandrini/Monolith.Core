namespace User.Domain.Exceptions
{
    public class UserEmailNotFoundException(Guid UserId) : Exception($"Email User with UserId '{UserId}' not found.") { }
}
