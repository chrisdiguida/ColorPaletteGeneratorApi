using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Models
{
    public class Palette : Entity
    {
        public string Name { get; set; }
        [StringLength(7, MinimumLength = 7)]
        [Required]
        public string BaseColor { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTime.UtcNow;
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
