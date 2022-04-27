using System;
using System.Security.Claims;

namespace iTechArt.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claims)
        {
            var id = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.Parse(id);
        }
    }
}