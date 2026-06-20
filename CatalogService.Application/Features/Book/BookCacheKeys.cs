namespace CatalogService.Application.Features.Book;

public static class BookCacheKeys
{
    public static string ById(long id) => $"books:{id}";
}
