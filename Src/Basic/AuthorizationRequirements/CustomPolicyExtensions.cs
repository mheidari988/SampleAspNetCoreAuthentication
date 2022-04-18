using Microsoft.AspNetCore.Authorization;

namespace Basic.AuthorizationRequirements
{
    public static class CustomPolicyExtensions
    {
        public static AuthorizationPolicyBuilder AddCustomPolicy(this AuthorizationPolicyBuilder builder, string customClaim)
        {
            builder.AddRequirements(new CustomPolicy(customClaim));
            return builder;
        }
    }
}
