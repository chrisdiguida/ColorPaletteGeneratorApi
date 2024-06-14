using ColorPaletteGeneratorApi.Models;

namespace ColorPaletteGeneratorApi.Data.Repositories.Interfaces
{
    public interface IAppUsersRepository
    {
        Task<bool> AppUserExists(Guid appUserId);
        Task<bool> AppUserExists(string email);
        void CreateAppUser(AppUser appUser);
        void DeleteAppUser(AppUser appUser);
        Task<AppUser> GetAppUser(Guid appUserRequesterId);
        Task<AppUser> GetAppUser(string email);
        void UpdateAppUser(AppUser appUser);
    }
}