namespace BookMarketWeb.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    
    public string NormalizedTitle { get; set; }
    public int PublishYear { get; set; }
    public int YearOfWriting { get; set; }
    public decimal Price { get; set; }
    
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}