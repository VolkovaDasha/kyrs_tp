using BookMarketWeb.Controllers;
using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookMarketWebTests;

public class BookControllerTests
{
	private readonly Mock<IRepository<Author>> _authorRepository = new Mock<IRepository<Author>>();
	private readonly Mock<IRepository<Book>> _bookRepository = new Mock<IRepository<Book>>();
	private readonly BookController _controller;

	public BookControllerTests()
	{
		_controller = new BookController(_bookRepository.Object, _authorRepository.Object);
	}

	[Fact]
	public async Task Get_ViewResult_WithIndex()
	{
		_bookRepository.Setup(p => p.GetAllAsync())
			.ReturnsAsync([]);
		
		var result = await _controller.Index();

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Get_ViewResult_WithDetails()
	{
		var bookId = Guid.NewGuid();
		_bookRepository.Setup(p => p.FindAsync(bookId))
			.ReturnsAsync(new Book());
		
		var result = await _controller.Details(bookId);

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Get_ViewResult_WithCreate()
	{
		var result = await _controller.Create();

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Post_RedirectToActionResult_WithCreate()
	{
		var bookId = Guid.NewGuid();
		_bookRepository.Setup(p => p.FindAsync(bookId))
			.ReturnsAsync(new Book());
		
		var result = await _controller.Create(new CreateBookDto()
		{
			Title = string.Empty
		});

		Assert.IsType<RedirectToActionResult>(result);
	}
	
	[Fact]
	public async Task Get_ViewResult_WithUpdate()
	{
		var bookId = Guid.NewGuid();
		
		_bookRepository.Setup(p => p.FindAsync(bookId))
			.ReturnsAsync(new Book());
		
		var result = await _controller.Update(bookId);

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Post_RedirectToActionResult_WithDelete()
	{
		var bookId = Guid.NewGuid();
		_bookRepository.Setup(p => p.FindAsync(bookId))
			.ReturnsAsync(new Book());

		var result = await _controller.Delete(bookId);

		Assert.IsType<RedirectToActionResult>(result);
	}
}