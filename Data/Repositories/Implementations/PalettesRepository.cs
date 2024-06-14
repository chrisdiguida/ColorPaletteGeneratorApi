using ColorPaletteGeneratorApi.Data.Repositories.Interfaces;
using ColorPaletteGeneratorApi.Extensions;
using ColorPaletteGeneratorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorPaletteGeneratorApi.Data.Repositories.Implementations
{
    public class PalettesRepository(AppDbContext dbContext) : IPalettesRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> Exists(Guid appUserRequesterId, string hexColor)
        {
            return await _dbContext.Palettes
                .AnyAsync(x => x.AppUserId == appUserRequesterId && x.BaseColor == hexColor);
        }

        public async Task<Palette> Get(Guid appUserRequesterId, Guid paletteId)
        {
            return await _dbContext.Palettes
                .FirstOrDefaultAsync(x => x.AppUserId == appUserRequesterId && x.Id == paletteId);
        }

        public async Task<List<Palette>> GetAll(Guid appUserRequesterId, string filter, int page)
        {
            string filterToApply = filter?.ToLower() ?? string.Empty;

            var query = _dbContext.Palettes.Where(x => x.AppUserId == appUserRequesterId);

            if (!string.IsNullOrEmpty(filterToApply))
            {
                query = query.Where(x => x.BaseColor.ToLower().Contains(filterToApply) || x.Name.ToLower().Contains(filterToApply));
            }

            return await query
                .OrderByDescending(x => x.CreationDate)
                .TakePage(page, 10)
                .ToListAsync();
        }

        public void Create(Palette palette)
        {
            _dbContext.Palettes.Add(palette);
        }

        public void Update(Palette palette)
        {
            _dbContext.Palettes.Update(palette);
        }

        public void Delete(Palette palette)
        {
            _dbContext.Palettes.Remove(palette);
        }
    }
}
