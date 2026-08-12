namespace CatalogService.UseCases.Book;

public static class BookCacheKeys
{
    public static string ById(long id) => $"books:{id}";
}
