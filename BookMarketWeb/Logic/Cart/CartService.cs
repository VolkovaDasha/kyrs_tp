using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWeb.Infrastructure.Sessions;

namespace BookMarketWeb.Logic.Cart;

public class CartService : ICartService
{
    private readonly ISessionService _sessionService;
    private readonly IRepository<Book> _bookRepository;

    public CartService(ISessionService sessionService, IRepository<Book> bookRepository)
    {
        _sessionService = sessionService;
        _bookRepository = bookRepository;
    }

    public async Task<Cart> AddToCartAsync(Guid bookId)
    {
        var book = await _bookRepository.FindAsync(bookId);
        if (book is null)
        {
            throw new InvalidDataException();
        }

        var cart = _sessionService.Get<Cart>("cart");
        if (cart is null)
        {
            cart = new Cart();
        }
        
        cart.AddItem(book, 1);
        
        _sessionService.Set("cart", cart);

        return cart;
    }

    public Cart GetCart()
    {
        var cart = _sessionService.Get<Cart>("cart");
        if (cart is null)
        {
            cart = new Cart();
            _sessionService.Set("cart", cart);
        }
        
        return cart;
    }
}