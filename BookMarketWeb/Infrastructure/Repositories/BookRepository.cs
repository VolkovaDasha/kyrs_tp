using BookMarketWeb.Domain.Entities;

namespace BookMarketWeb.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>
{
    public BookRepository(MarketDbContext context) : base(context)
    {
    }
}