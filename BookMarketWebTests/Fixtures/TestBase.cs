using BookMarketWeb.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookMarketWebTests.Fixtures;

public abstract class TestBase
{
	public MarketDbContext CreateContext()
	{
		var unique = Guid.NewGuid();
		var options = new DbContextOptionsBuilder<MarketDbContext>().UseInMemoryDatabase($"MarketDb_{unique}").Options;
		return new MarketDbContext(options);
	}
}