namespace CatalogService.Application.UseCases.Book.ViewModels;

public class BookViewModel
{
    public BookViewModel(string title , string author , long stock , int price)
    {
        Title = title;
        Author = author;
        Stock = stock;
        Price = price;
    }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public long Stock { get; set; }
    public int Price { get; set; }
}
