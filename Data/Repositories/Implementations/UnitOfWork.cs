using ColorPaletteGeneratorApi.Data.Repositories.Interfaces;

namespace ColorPaletteGeneratorApi.Data.Repositories.Implementations
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
