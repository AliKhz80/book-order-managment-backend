namespace CatalogService.Application.UseCases.Book;

public static class BookCacheKeys
{
    public static string ById(long id) => $"books:{id}";
}
