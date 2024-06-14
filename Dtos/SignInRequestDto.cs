using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Dtos
{
    public class SignInRequestDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }

    }
}