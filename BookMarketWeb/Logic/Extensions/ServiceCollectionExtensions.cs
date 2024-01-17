using BookMarketWeb.Logic.Books;
using BookMarketWeb.Logic.Cart;

namespace BookMarketWeb.Logic.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCart(this IServiceCollection services)
    {
        services.AddScoped<ICartService, CartService>();
    }
    
    public static void AddSearch(this IServiceCollection services)
    {
        services.AddScoped<BookFinder>();
    }
}