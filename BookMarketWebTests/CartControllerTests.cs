using BookMarketWeb.Controllers;
using BookMarketWeb.Logic.Cart;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookMarketWebTests;

public class CartControllerTests
{
	private readonly Mock<ICartService> _cartService = new Mock<ICartService>();
	private readonly CartController _controller;

	public CartControllerTests()
	{
		_controller = new CartController(_cartService.Object);
	}

	[Fact]
	public void Get_ViewResult_WithGet()
	{
		_cartService.Setup(p => p.GetCart())
			.Returns(new Cart());
		
		var result = _controller.Get();

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public void Get_PartialViewResult_WithCartPartial()
	{
		_cartService.Setup(p => p.GetCart())
			.Returns(new Cart());
		
		var result = _controller.CartPartial();

		Assert.IsType<PartialViewResult>(result);
	}
	
	[Fact]
	public async Task Post_PartialViewResult_WithAddToCart()
	{
		var bookId = Guid.NewGuid();
		_cartService.Setup(p => p.AddToCartAsync(bookId))
			.ReturnsAsync(new Cart());
		
		var result = await _controller.AddToCart(bookId);

		Assert.IsType<PartialViewResult>(result);
	}
	
	[Fact]
	public async Task Delete_PartialViewResult_WithAddToCart()
	{
		var bookId = Guid.NewGuid();
		_cartService.Setup(p => p.AddToCartAsync(bookId))
			.ReturnsAsync(new Cart());
		
		var result = await _controller.AddToCart(bookId);

		Assert.IsType<PartialViewResult>(result);
	}
}