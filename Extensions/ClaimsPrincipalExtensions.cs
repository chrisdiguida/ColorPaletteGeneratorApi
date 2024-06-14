using System.Security.Claims;

namespace ColorPaletteGeneratorApi.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetAppUserId(this ClaimsPrincipal claimsPrincipal)
        {
            Claim nameIdentifier = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Guid.Parse(nameIdentifier.Value);
        }
    }
}
