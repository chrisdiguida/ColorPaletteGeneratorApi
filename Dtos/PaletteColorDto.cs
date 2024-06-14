using System.Drawing;

namespace ColorPaletteGeneratorApi.Dtos
{
    public class PaletteColorDto
    {
        public string Color { get; set; }
        public string Boldness { get; set; }

        public static PaletteColorDto GenerateFromColor(Color color, int brightness)
        {
            string newColorHex = ColorTranslator.ToHtml(color);
            string boldness = $"{brightness}00";
            return new()
            {
                Color = newColorHex,
                Boldness = boldness,
            };
        }
    }
}
