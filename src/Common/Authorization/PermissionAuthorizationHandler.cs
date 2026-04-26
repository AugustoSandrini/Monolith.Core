using Microsoft.AspNetCore.Authorization;

namespace Common.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var hasPermission = context.User.Claims
            .Where(c => c.Type == "permissions")
            .Any(c => requirement.Permissions.Contains(c.Value));

        if (hasPermission)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
