using ColorPaletteGeneratorApi.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Dtos
{
    public class CreatePaletteRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [HexColor]
        public string BaseColor { get; set; }
    }
}