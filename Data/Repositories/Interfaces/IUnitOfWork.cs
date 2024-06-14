namespace ColorPaletteGeneratorApi.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}