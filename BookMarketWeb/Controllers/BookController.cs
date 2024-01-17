using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWeb.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketWeb.Controllers;

public class BookController : Controller
{
    private readonly IRepository<Book> _repository;
    private readonly IRepository<Author> _authorRepository;

    public BookController(IRepository<Book> repository, IRepository<Author> authorRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var books = await _repository.GetAllAsync();

        var dtos = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            PublishYear = b.PublishYear,
            YearOfWriting = b.YearOfWriting,
            Price = b.Price
        });
        
        return View(dtos);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var book = await _repository.FindAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var authors = await _authorRepository.GetAllAsync();

        ViewBag.Authors = authors;
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto bookDto)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = bookDto.Title,
            NormalizedTitle = bookDto.Title.ToLower().Trim(),
            PublishYear = bookDto.PublishYear,
            YearOfWriting = bookDto.YearOfWriting,
            Price = bookDto.Price,
            AuthorId = bookDto.AuthorId,
        };

        _repository.Create(book);
        await _repository.SaveAsync();
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var book = await _repository.FindAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        var dto = new UpdateBookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublishYear = book.PublishYear,
            YearOfWriting = book.YearOfWriting,
            Price = book.Price
        };

        return View(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(UpdateBookDto dto)
    {
        var book = await _repository.FindAsync(dto.Id);
        if (book is null)
        {
            return NotFound();
        }

        book.Title = dto.Title;
        book.NormalizedTitle = dto.Title.ToLower().Trim();
        book.PublishYear = dto.PublishYear;
        book.YearOfWriting = dto.YearOfWriting;
        book.Price = dto.Price;
        book.AuthorId = dto.AuthorId;
        _repository.Update(book);
        await _repository.SaveAsync();

        var updated = new UpdateBookDto()
        {
            Id = book.Id,
            Title = book.Title,
            PublishYear = book.PublishYear,
            YearOfWriting = book.YearOfWriting,
            Price = book.Price
        };
        
        return View(updated);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var book = await _repository.FindAsync(id);
        if (book is null)
        {
            return NotFound();
        }
        
        _repository.Remove(book);
        await _repository.SaveAsync();
        
        return RedirectToAction("Index");
    }
}