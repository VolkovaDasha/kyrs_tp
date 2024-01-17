namespace BookMarketWeb.Logic.Cart;

public interface ICartService
{
    Task<Cart> AddToCartAsync(Guid bookId);
    
    Cart GetCart();
}