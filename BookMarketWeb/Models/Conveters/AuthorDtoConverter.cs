using BookMarketWeb.Domain.Entities;

namespace BookMarketWeb.Models.Conveters;

public static class AuthorDtoConverter
{
    public static AuthorDto Convert(Author book)
    {
        return new AuthorDto
        {
            Id = book.Id,
            Name = book.Name
        };
    }
}