using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Services.Interfaces;
using System.Drawing;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class PaletteGeneratorServices() : IPaletteGeneratorServices
    {
        private const int _paletteColors = 9;

        /// <summary>
        /// Generates a color palette based on the provided hex color.
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds the base color to the list of colors at the appropriate index based on brightness.
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="baseColor"></param>
        /// <param name="brightness"></param>
        private static void AddBaseColor(List<Color> colors, Color baseColor, int brightness)
        {
            int index = _paletteColors - brightness;
            colors[index] = baseColor;
        }

        /// <summary>
        /// Generates a list of lighter colors based on the base color and its brightness.
        /// </summary>
        /// <param name="baseColor"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates a list of darker colors based on the base color and its brightness.
        /// </summary>
        /// <param name="baseColor"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Defines the scale factor for changing color brightness based on the correction factor.
        /// </summary>
        /// <param name="correctionFactor"></param>
        /// <returns></returns>
        private static float DefineChangeColorBrightnessScale(int correctionFactor)
        {
            float scale = (float)correctionFactor / 10;
            return scale;

        }

        /// <summary>
        /// Defines the brightness of a given color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Changes the brightness of a given color based on the correction factor.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="correctionFactor"></param>
        /// <returns></returns>
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
