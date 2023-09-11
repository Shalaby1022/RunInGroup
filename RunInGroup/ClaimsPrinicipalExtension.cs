using System.Security.Claims;

namespace RunInGroup
{
    public static class ClaimsPrinicipalExtension
    {
       public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
