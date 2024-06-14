using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Services.Interfaces;
using System.Drawing;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class PaletteGeneratorServices() : IPaletteGeneratorServices
    {
        private const int _paletteColors = 9;

        public List<PaletteColorDto> GeneratePalette(string hexColor)
        {
            List<Color> colors = [];
            Color baseColor = ColorTranslator.FromHtml(hexColor);
            int brightness = DefineColorBrightness(baseColor);

            List<Color> lightColors = GenerateLighterColors(baseColor, brightness);
            List<Color> darkColors = GenerateDarkerColors(baseColor, brightness);
            colors.AddRange(lightColors);
            colors.AddRange(darkColors);
            AddBaseColor(colors, baseColor, brightness);

            List<PaletteColorDto> paletteColors = colors
                .Select((x, index) => PaletteColorDto.GenerateFromColor(x, index + 1))
                .ToList();

            return paletteColors;
        }

        private static void AddBaseColor(List<Color> colors, Color baseColor, int brightness)
        {
            int index = _paletteColors - brightness;
            colors[index] = baseColor;
        }

        private static List<Color> GenerateLighterColors(Color baseColor, int brightness)
        {
            List<Color> colors = [];
            int lightColorsToCreate = _paletteColors - brightness;

            for (int i = lightColorsToCreate; i >= 1; i--)
            {
                float scale = DefineChangeColorBrightnessScale(i);
                Color newColor = ChangeColorBrightness(baseColor, scale);
                colors.Add(newColor);
            }
            return colors;
        }

        private static List<Color> GenerateDarkerColors(Color baseColor, int brightness)
        {
            List<Color> colors = [];
            int darkColorsToProduce = _paletteColors - (_paletteColors - brightness);

            for (int i = 1; i <= darkColorsToProduce; i++)
            {
                float scale = DefineChangeColorBrightnessScale(i);
                Color newColor = ChangeColorBrightness(baseColor, -scale);
                colors.Add(newColor);
            }
            return colors;
        }

        private static float DefineChangeColorBrightnessScale(int correctionFactor)
        {
            float scale = (float)correctionFactor / 10;
            return scale;

        }

        private static int DefineColorBrightness(Color color)
        {
            int brightness = (int)(color.GetBrightness() * 10);
            if (brightness == 0)
            {
                return 1;
            }

            if (brightness == 10)
            {
                return _paletteColors;
            }

            return brightness;
        }

        private static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
