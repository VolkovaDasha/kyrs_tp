using BookMarketWeb.Domain.Entities;

namespace BookMarketWeb.Infrastructure.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Query { get; }

    Task<List<T>> GetAllAsync();
    ValueTask<T?> FindAsync(Guid id);
    T Create(T entity);
    IEnumerable<T> Create(IEnumerable<T> entities);
    T Update(T entity);
    void Remove(T entity);
    void Remove(IEnumerable<T> entities);
    Task SaveAsync();
}