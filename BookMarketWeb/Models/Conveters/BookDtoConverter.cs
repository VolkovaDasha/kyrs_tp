using BookMarketWeb.Domain.Entities;

namespace BookMarketWeb.Models.Conveters;

public static class BookDtoConverter
{
    public static BookDto Convert(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishYear = book.PublishYear,
            YearOfWriting = book.YearOfWriting,
            Price = book.Price
        };
    }
}