using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SM_ApplicationLayer.Extensions
{
   public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static int GetUserId (this ClaimsPrincipal principal)
        {
            return (Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public static string GetUserName (this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);

        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal,string id)
        {
            var currentUserId = GetUserId(principal).ToString();
            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }
    }
}
