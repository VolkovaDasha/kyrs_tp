using BookMarketWeb.Controllers;
using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookMarketWebTests;

public class AuthorControllerTests
{
	private readonly Mock<IRepository<Author>> _authorRepository = new Mock<IRepository<Author>>();
	private readonly AuthorController _controller;

	public AuthorControllerTests()
	{
		_controller = new AuthorController(_authorRepository.Object);
	}

	[Fact]
	public async Task Get_ViewResult_WithIndex()
	{
		_authorRepository.Setup(p => p.GetAllAsync())
			.ReturnsAsync([]);
		
		var result = await _controller.Index();

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Get_ViewResult_WithDetails()
	{
		var authorId = Guid.NewGuid();
		_authorRepository.Setup(p => p.FindAsync(authorId))
			.ReturnsAsync(new Author());
		
		var result = await _controller.Details(authorId);

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public void Get_ViewResult_WithCreate()
	{
		var result = _controller.Create();

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Post_RedirectToActionResult_WithCreate()
	{
		var authorId = Guid.NewGuid();
		_authorRepository.Setup(p => p.FindAsync(authorId))
			.ReturnsAsync(new Author());
		
		var result = await _controller.Create(new CreateAuthorDto());

		Assert.IsType<RedirectToActionResult>(result);
	}
	
	[Fact]
	public async Task Get_ViewResult_WithUpdate()
	{
		var authorId = Guid.NewGuid();
		
		_authorRepository.Setup(p => p.FindAsync(authorId))
			.ReturnsAsync(new Author());
		
		var result = await _controller.Update(authorId);

		Assert.IsType<ViewResult>(result);
	}
	
	[Fact]
	public async Task Post_RedirectToActionResult_WithDelete()
	{
		var authorId = Guid.NewGuid();
		_authorRepository.Setup(p => p.FindAsync(authorId))
			.ReturnsAsync(new Author());

		var result = await _controller.Delete(authorId);

		Assert.IsType<RedirectToActionResult>(result);
	}
}