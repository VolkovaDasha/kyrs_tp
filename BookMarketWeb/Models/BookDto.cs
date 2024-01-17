namespace BookMarketWeb.Models;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public int YearOfWriting { get; set; }
    public decimal Price { get; set; }
}

public class CreateBookDto
{
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public int YearOfWriting { get; set; }
    public decimal Price { get; set; }
    public Guid AuthorId { get; set; }
}

public class UpdateBookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public int YearOfWriting { get; set; }
    public decimal Price { get; set; }
    public Guid AuthorId { get; set; }
}