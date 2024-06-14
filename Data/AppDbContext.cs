using ColorPaletteGeneratorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorPaletteGeneratorApi.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Palette> Palettes { get; set; }
    }
}
