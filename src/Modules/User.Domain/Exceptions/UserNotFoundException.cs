namespace User.Domain.Exceptions
{
    public class UserNotFoundException(Guid UserId) : Exception($"User with UserId '{UserId}' not found.") { }
}
