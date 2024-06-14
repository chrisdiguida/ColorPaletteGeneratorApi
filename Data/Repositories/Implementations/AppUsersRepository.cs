using ColorPaletteGeneratorApi.Data.Repositories.Interfaces;
using ColorPaletteGeneratorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorPaletteGeneratorApi.Data.Repositories.Implementations
{
    public class AppUsersRepository(AppDbContext dbContext) : IAppUsersRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> AppUserExists(Guid appUserId)
        {
            return await _dbContext.AppUsers.AnyAsync(x => x.Id == appUserId);
        }

        public async Task<bool> AppUserExists(string email)
        {
            return await _dbContext.AppUsers.AnyAsync(x => x.Email == email);
        }

        public async Task<AppUser> GetAppUser(string email)
        {
            return await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AppUser> GetAppUser(Guid appUserRequesterId)
        {
            return await _dbContext.AppUsers
                .FirstOrDefaultAsync(x => x.Id == appUserRequesterId);
        }

        public void CreateAppUser(AppUser appUser)
        {
            _dbContext.AppUsers.Add(appUser);
        }

        public void UpdateAppUser(AppUser appUser)
        {
            _dbContext.AppUsers.Update(appUser);
        }

        public void DeleteAppUser(AppUser appUser)
        {
            _dbContext.AppUsers.Remove(appUser);
        }
    }
}
