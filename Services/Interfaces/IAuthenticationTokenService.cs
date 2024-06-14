using ColorPaletteGeneratorApi.Models;

namespace ColorPaletteGeneratorApi.Services.Interfaces
{
    public interface IAuthenticationTokenService
    {
        string GenerateAuthenticationToken(AppUser appUser);
    }
}