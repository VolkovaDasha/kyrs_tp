using BookMarketWeb.Logic.Cart;
using BookMarketWeb.Models;
using BookMarketWeb.Models.Conveters;

using Microsoft.AspNetCore.Mvc;

namespace BookMarketWeb.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var cart = _cartService.GetCart();

        var dto = CartDtoConverter.Convert(cart);

        return View(dto);
    }

    [HttpGet]
    public IActionResult CartPartial()
    {
        var cart = _cartService.GetCart();

        var dto = CartDtoConverter.Convert(cart);

        return PartialView("_CartPartial", dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid bookId)
    {
        var cart = await _cartService.AddToCartAsync(bookId);

        var dto = CartDtoConverter.Convert(cart);
        
        return PartialView("_CartPartial", dto);
    }
}