using System.Security.Claims;

namespace API.Extensions
{
    // Extension for validating user by username from token used to identify user
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}