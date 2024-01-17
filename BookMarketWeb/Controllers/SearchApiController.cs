using BookMarketWeb.Logic.Books;
using BookMarketWeb.Models.Conveters;

using Microsoft.AspNetCore.Mvc;

namespace BookMarketWeb.Controllers;

[Route("book-search")]
public class BookSearchApiController : ControllerBase
{
    private readonly BookFinder _bookFinder;

    public BookSearchApiController(BookFinder bookFinder)
    {
        _bookFinder = bookFinder;
    }

    [HttpGet("query")]
    public async Task<IActionResult> SearchBooks(string? title = null, int? yearOfWriting = null, decimal? lessThanPrice = null)
    {
        var books = await _bookFinder.SearchAsync(title, yearOfWriting, lessThanPrice);

        var dtos = books.Select(BookDtoConverter.Convert);
        
        return Ok(dtos);
    }
}