using Microsoft.AspNetCore.Authorization;

namespace Basic.AuthorizationRequirements
{
    public class CustomPolicyHandler : AuthorizationHandler<CustomPolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomPolicy requirement)
        {
            var hasClaim = context.User.Claims.Any(c => c.Type == requirement.ClaimType);

            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
