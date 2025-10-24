using System.Security.Claims;

namespace BankingAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    private const string SubjectClaim = "sub";
    
    public static string Sub(this ClaimsPrincipal claimsPrincipal)
    {
        var sub = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == SubjectClaim)?.Value;

        return sub;
    }
}