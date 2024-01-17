using System.Linq.Expressions;

using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Extensions;
using BookMarketWeb.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BookMarketWeb.Logic.Books;

public class BookFinder
{
    private readonly IRepository<Book> _bookRepository;
    private readonly SearchBuilder _searchBuilder;
    
    public BookFinder(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
        _searchBuilder = new ConcreteSearchBuilder();
    }

    public async Task<List<Book>> SearchAsync(string? title = null, int? yearOfWriting = null, decimal? lessThanPrice = null)
    {
        var filter = _searchBuilder
            .SetTitleFilter(title)
            .SetYearOfWritingFilter(yearOfWriting)
            .SetLessThanPriceFilter(lessThanPrice)
            .GetResult();

        var books = await _bookRepository.Query.Where(filter).ToListAsync();

        return books;
    }
}

public abstract class SearchBuilder
{
    public abstract SearchBuilder SetTitleFilter(string? title);

    public abstract SearchBuilder SetYearOfWritingFilter(int? yearOfWriting);

    public abstract SearchBuilder SetLessThanPriceFilter(decimal? lessThanPrice);

    public abstract Expression<Func<Book, bool>> GetResult();
}

public class ConcreteSearchBuilder : SearchBuilder
{
    private string? _titleFilter;
    private bool _filterByTitle = false;
    
    private int? _yearOfWritingFilter;
    private bool _filterYearOfWriting = false;

    private decimal? _lessThanPriceFilter;
    private bool _filterByPrice = false;
    
    public override SearchBuilder SetTitleFilter(string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return this;
        }
        
        _titleFilter = title.ToLower().Trim();
        _filterByTitle = true;
        
        return this;
    }
    
    public override SearchBuilder SetYearOfWritingFilter(int? yearOfWriting)
    {
        if (yearOfWriting is null)
        {
            return this;
        }
        
        _yearOfWritingFilter = yearOfWriting;
        _filterYearOfWriting = true;
        
        return this;
    }
    
    public override SearchBuilder SetLessThanPriceFilter(decimal? lessThanPrice)
    {
        if (lessThanPrice is null)
        {
            return this;
        }
        
        _lessThanPriceFilter = lessThanPrice;
        _filterByPrice = true;
        
        return this;
    }

    public override Expression<Func<Book, bool>> GetResult()
    {
        Expression<Func<Book, bool>> filter = book => true;

        if (_filterByTitle)
        {
            filter = book => book.NormalizedTitle.Contains(_titleFilter);
        }
        
        if (_filterYearOfWriting)
        {
            Expression<Func<Book, bool>> filterByYear = book => book.YearOfWriting == _yearOfWritingFilter;

            filter = filter.And(filterByYear);
        }

        if (_filterByPrice)
        {
            Expression<Func<Book, bool>> filterByPrice = book => book.Price < _lessThanPriceFilter;

            filter = filter.And(filterByPrice);
        }
        
        return filter;
    }
}