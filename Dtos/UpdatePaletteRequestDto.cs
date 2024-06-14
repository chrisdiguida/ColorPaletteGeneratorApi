using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Dtos
{
    public class UpdatePaletteRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
    }
}