using BookMarketWeb.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace BookMarketWeb.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly MarketDbContext Context;

    protected BaseRepository(MarketDbContext context)
    {
        Context = context;
    }

    public IQueryable<T> Query => Context.Set<T>();

    public async Task<List<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async ValueTask<T?> FindAsync(Guid id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public T Create(T entity)
    {
        Context.Set<T>().Add(entity);

        return entity;
    }

    public IEnumerable<T> Create(IEnumerable<T> entities)
    {
        var list = entities.ToList();
        Context.Set<T>().AddRange(list);

        return list;
    }

    public T Update(T entity)
    {
        Context.Set<T>().Update(entity);

        return entity;
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void Remove(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }

    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}