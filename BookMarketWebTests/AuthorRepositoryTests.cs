using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWebTests.Fixtures;

namespace BookMarketWebTests;

public class AuthorRepositoryTests : TestBase
{ 
	[Fact]
	public async Task Create_AddsNew()
	{
		using var context = CreateContext();
		var repository = new AuthorRepository(context);
		
		var author = new Author
		{
			Id = Guid.NewGuid(),
			Name = "Test"
		};

		repository.Create(author);
		await repository.SaveAsync();
		var created = await context.Authors.FindAsync(author.Id);

		Assert.NotNull(created);
		Assert.Equal(author.Name, created.Name);
	}
	
	[Fact]
	public async Task FindAsync_FindCreated()
	{
		using var context = CreateContext();
		var repository = new AuthorRepository(context);
		
		var author = new Author
		{
			Id = Guid.NewGuid(),
			Name = "Test"
		};

		context.Authors.Add(author);
		await context.SaveChangesAsync();

		var created = await repository.FindAsync(author.Id);

		Assert.NotNull(created);
		Assert.Equal(author.Name, created.Name);
	}
	
	[Fact]
	public async Task GetAllAsync_FindCreated()
	{
		using var context = CreateContext();
		var repository = new AuthorRepository(context);
		
		var author = new Author
		{
			Id = Guid.NewGuid(),
			Name = "Test"
		};

		context.Authors.Add(author);
		await context.SaveChangesAsync();

		var list = await repository.GetAllAsync();

		Assert.Single(list);
		var created = list.First();
		Assert.NotNull(created);
		Assert.Equal(author.Name, created.Name);
	}
	
	[Fact]
	public async Task Delete_DeleteCreated()
	{
		using var context = CreateContext();
		var repository = new AuthorRepository(context);
		
		var author = new Author
		{
			Id = Guid.NewGuid(),
			Name = "Test"
		};

		context.Authors.Add(author);
		await context.SaveChangesAsync();

		repository.Remove(author);
		await repository.SaveAsync();

		var deleted = await context.Authors.FindAsync(author.Id);

		Assert.Null(deleted);
	}
}