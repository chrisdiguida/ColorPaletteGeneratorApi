using ColorPaletteGeneratorApi.Dtos;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public interface IPalettesServices
    {
        Task Create(Guid appUserRequesterId, CreatePaletteRequestDto request);
        Task Delete(Guid appUserRequesterId, Guid paletteId);
        Task<PaletteDto> GeneratePalette(Guid appUserRequesterId, string hexColor);
        Task<List<PaletteDto>> GetAll(Guid appUserRequesterId, string filter, int page);
        Task UpdateName(Guid appUserRequesterId, UpdatePaletteRequestDto request);
    }
}