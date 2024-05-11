using System.Security.Claims;

namespace GraniteExpress.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.FindFirstValue<string>(ClaimTypes.Name);

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.FindFirstValue<string>(ClaimTypes.NameIdentifier);
    }

    public static class PrincipalExtensions
    {
        public static T FindFirstValue<T>(this ClaimsPrincipal principal, string claimType)
        {
            try
            {
                var claim = principal.FindFirst(claimType);
                return (T)Convert.ChangeType(claim.Value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
