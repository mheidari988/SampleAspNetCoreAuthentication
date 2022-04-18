using Microsoft.AspNetCore.Authorization;

namespace Basic.AuthorizationRequirements
{
    public class CustomPolicy : IAuthorizationRequirement
    {
        public CustomPolicy(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }
    }
}
