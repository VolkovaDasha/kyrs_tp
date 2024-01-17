using BookMarketWeb.Logic.Cart;

namespace BookMarketWeb.Models.Conveters;

public static class CartDtoConverter
{
    public static CartDto Convert(Cart cart)
    {
        return new CartDto
        {
            TotalAmount = cart.ComputeTotalValue(),
            Lines = cart.Lines.Select(p => new CartLineDto
            {
                Book = new BookDto
                {
                    Id = p.Book.Id,
                    Title = p.Book.Title,
                    PublishYear = p.Book.PublishYear,
                    YearOfWriting = p.Book.YearOfWriting,
                    Price = p.Book.Price
                },
                Quantity = p.Quantity,
                Amount = p.Quantity * p.Book.Price
            }).ToList()
        };
    }
}