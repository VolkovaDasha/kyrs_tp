using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWeb.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketWeb.Controllers;

public class AuthorController : Controller
{
    private readonly IRepository<Author> _repository;

    public AuthorController(IRepository<Author> authorRepository)
    {
        _repository = authorRepository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var authors = await _repository.GetAllAsync();

        var dtos = authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name });
        
        return View(dtos);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var author = await _repository.FindAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        var dto = new AuthorDto() { Id = author.Id, Name = author.Name };

        return View(dto);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorDto authorDto)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = authorDto.Name
        };

        _repository.Create(author);
        await _repository.SaveAsync();
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var author = await _repository.FindAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        var dto = new UpdateAuthorDto
        {
            Id = author.Id,
            Name = author.Name
        };

        return View(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(UpdateAuthorDto dto)
    {
        var author = await _repository.FindAsync(dto.Id);
        if (author is null)
        {
            return NotFound();
        }

        author.Name = dto.Name;
        _repository.Update(author);
        await _repository.SaveAsync();

        var updated = new UpdateAuthorDto()
        {
            Id = author.Id,
            Name = author.Name
        };
        
        return View(updated);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var author = await _repository.FindAsync(id);
        if (author is null)
        {
            return NotFound();
        }
        
        _repository.Remove(author);
        await _repository.SaveAsync();
        
        return RedirectToAction("Index");
    }
}