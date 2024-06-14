namespace ColorPaletteGeneratorApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> TakePage<T>(this IQueryable<T> query, int pageNumber, int pageSize = 10)
        {
            int entitiesToSkip = (pageNumber - 1) * pageSize;

            return query.Skip(entitiesToSkip).Take(pageSize);
        }
    }
}
