using BookMarketWeb.Domain.Entities;

namespace BookMarketWeb.Infrastructure.Repositories;

public class AuthorRepository : BaseRepository<Author>
{
    public AuthorRepository(MarketDbContext context) : base(context)
    {
    }
}