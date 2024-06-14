namespace ColorPaletteGeneratorApi.Models
{
    public class AppUser : Entity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastSignIn { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastAppAccess { get; set; }
        public List<Palette> Palettes { get; set; }
    }
}
