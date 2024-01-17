using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWebTests.Fixtures;

namespace BookMarketWebTests;

public class BookRepositoryTests : TestBase
{ 
	[Fact]
	public async Task Create_AddsNew()
	{
		using var context = CreateContext();
		var repository = new BookRepository(context);

		var author = await CreateAuthor(context);
		
		var book = new Book
		{
			Id = Guid.NewGuid(),
			Title = "Test",
			NormalizedTitle = "test",
			AuthorId = author.Id
		};

		repository.Create(book);
		await repository.SaveAsync();
		var created = await context.Books.FindAsync(book.Id);

		Assert.NotNull(created);
		Assert.Equal(book.Title, created.Title);
	}
	
	[Fact]
	public async Task FindAsync_FindCreated()
	{
		using var context = CreateContext();
		var repository = new BookRepository(context);
		
		var author = await CreateAuthor(context);
		
		var book = new Book
		{
			Id = Guid.NewGuid(),
			Title = "Test",
			NormalizedTitle = "test",
			AuthorId = author.Id
		};

		context.Books.Add(book);
		await context.SaveChangesAsync();

		var created = await repository.FindAsync(book.Id);

		Assert.NotNull(created);
		Assert.Equal(book.Title, created.Title);
	}
	
	[Fact]
	public async Task GetAllAsync_GetAllAsync()
	{
		using var context = CreateContext();
		var repository = new BookRepository(context);
		
		var author = await CreateAuthor(context);
		
		var book = new Book
		{
			Id = Guid.NewGuid(),
			Title = "Test",
			NormalizedTitle = "test",
			AuthorId = author.Id
		};

		context.Books.Add(book);
		await context.SaveChangesAsync();

		var list = await repository.GetAllAsync();

		Assert.Single(list);
		var created = list.First();
		Assert.NotNull(created);
		Assert.Equal(book.Title, created.Title);
	}
	
	[Fact]
	public async Task Delete_DeleteCreated()
	{
		using var context = CreateContext();
		var repository = new BookRepository(context);
		
		var author = await CreateAuthor(context);
		
		var book = new Book
		{
			Id = Guid.NewGuid(),
			Title = "Test",
			NormalizedTitle = "test",
			AuthorId = author.Id
		};

		context.Books.Add(book);
		await context.SaveChangesAsync();

		repository.Remove(book);
		await repository.SaveAsync();

		var deleted = await context.Books.FindAsync(book.Id);

		Assert.Null(deleted);
	}

	private async Task<Author> CreateAuthor(MarketDbContext context)
	{
		var author = new Author()
		{
			Id = Guid.NewGuid(),
			Name = "Test"
		};
		
		context.Authors.Add(author);
		await context.SaveChangesAsync();

		return author;
	}
}