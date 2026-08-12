namespace CatalogService.UseCases.Book.ViewModels;

public class BookViewModel
{
    public BookViewModel(long id, string title, string author, long stock, int price)
    {
        Id = id;
        Title = title;
        Author = author;
        Stock = stock;
        Price = price;
    }

    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public long Stock { get; set; }
    public int Price { get; set; }
}
