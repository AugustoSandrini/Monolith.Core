namespace WebBff.Endpoints.Routes
{
    internal class UsersRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/Users";

        internal const string UserId = "UserId";


        internal const string CreateUser = $"{BaseUri}/create-user";
        internal const string DeleteUser = $"{BaseUri}/{{{UserId}:guid}}";
        internal const string UpdateProfile = $"{BaseUri}/profile";
        internal const string UpsertAddress = $"{BaseUri}/address";

        internal const string GetUserById = $"{BaseUri}";
    }
}
