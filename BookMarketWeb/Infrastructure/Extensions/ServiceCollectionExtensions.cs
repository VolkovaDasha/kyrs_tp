using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;
using BookMarketWeb.Infrastructure.Sessions;

namespace BookMarketWeb.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IRepository<Book>, BookRepository>()
            .AddScoped<IRepository<Author>, AuthorRepository>();
    }
    
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<ISessionService, SessionService>();
    }
}