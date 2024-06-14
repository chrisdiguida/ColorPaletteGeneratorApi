using ColorPaletteGeneratorApi.Models;

namespace ColorPaletteGeneratorApi.Data.Repositories.Interfaces
{
    public interface IPalettesRepository
    {
        void Create(Palette palette);
        void Delete(Palette palette);
        Task<bool> Exists(Guid appUserRequesterId, string hexColor);
        Task<Palette> Get(Guid appUserRequesterId, Guid paletteId);
        Task<List<Palette>> GetAll(Guid appUserRequesterId, string filter, int page);
        void Update(Palette palette);
    }
}