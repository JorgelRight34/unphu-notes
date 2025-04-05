using System;
using System.Security.Claims;

namespace api.Extensions;

public static class ClaimsExtensions
{
    public static string GetUsername(this ClaimsPrincipal claims)
    {
        return claims.FindFirstValue(ClaimTypes.GivenName) ?? throw new ArgumentNullException("Username claim not found.");
    }
}
