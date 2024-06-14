using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Dtos
{
    public class GetCurrentAppUserResponseDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}