using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BookMarketWeb.Infrastructure.Configurations;

public class Seed
{
    public async Task SeedDataAsync(IServiceScope scope)
    {
        var authorRepository = scope.ServiceProvider.GetRequiredService<IRepository<Author>>();
        var bookRepository = scope.ServiceProvider.GetRequiredService<IRepository<Book>>();

        if (!await authorRepository.Query.AnyAsync())
        {
            authorRepository.Create(_author);
            await authorRepository.SaveAsync();
        }
        
        if (!await bookRepository.Query.AnyAsync())
        {
            bookRepository.Create(_books);
            await bookRepository.SaveAsync();
        }
    }

    private static readonly Author _author = new() { Id = Guid.NewGuid(), Name = "Джоан Кэтлин Роулинг" };

    private static readonly List<Book> _books =
    [
        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и философский камень",
            NormalizedTitle = "Гарри Поттер и философский камень".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 1990,
            Price = 510,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и узник Азкабана",
            NormalizedTitle = "Гарри Поттер и узник Азкабана".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 1991,
            Price = 520,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и Кубок Огня",
            NormalizedTitle = "Гарри Поттер и Кубок Огня".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 1992,
            Price = 530,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и Орден Феникса",
            NormalizedTitle = "Гарри Поттер и Орден Феникса".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 1995,
            Price = 540,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и Принц-полукровка",
            NormalizedTitle = "Гарри Поттер и Принц-полукровка".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 1997,
            Price = 550,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и Дары смерти",
            NormalizedTitle = "Гарри Поттер и Дары смерти".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 1999,
            Price = 550,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и Проклятое дитя",
            NormalizedTitle = "Гарри Поттер и Проклятое дитя".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 2003,
            Price = 560,
            AuthorId = _author.Id
        },

        new Book
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и Тайная комната",
            NormalizedTitle = "Гарри Поттер и Тайная комната".ToLower(),
            PublishYear = 2022,
            YearOfWriting = 2006,
            Price = 570,
            AuthorId = _author.Id
        }
    ];
}