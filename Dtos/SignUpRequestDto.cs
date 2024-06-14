using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Dtos
{
    public class SignUpRequestDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
        [StringLength(100, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }
}