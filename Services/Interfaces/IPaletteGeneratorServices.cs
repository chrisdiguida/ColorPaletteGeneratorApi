using ColorPaletteGeneratorApi.Dtos;

namespace ColorPaletteGeneratorApi.Services.Interfaces
{
    public interface IPaletteGeneratorServices
    {
        List<PaletteColorDto> GeneratePalette(string hexColor);
    }
}