using BookMarketWeb.Controllers;
using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookMarketWebTests;

public class HomeControllerTests
{
	private readonly Mock<IRepository<Book>> _bookRepository = new Mock<IRepository<Book>>();
	private readonly HomeController _controller;

	public HomeControllerTests()
	{
		_controller = new HomeController(_bookRepository.Object);
	}

	[Fact]
	public async Task Get_ViewResult_WithIndex()
	{
		_bookRepository.Setup(p => p.GetAllAsync())
			.ReturnsAsync([]);
		
		var result = await _controller.Index();

		Assert.IsType<ViewResult>(result);
	}
}