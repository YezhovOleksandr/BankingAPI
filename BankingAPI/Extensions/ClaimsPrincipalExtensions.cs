using System.Security.Claims;

namespace BankingAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    private const string SubjectClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    
    public static string Sub(this ClaimsPrincipal claimsPrincipal)
    {
        var sub = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == SubjectClaim)?.Value;

        return sub;
    }
}