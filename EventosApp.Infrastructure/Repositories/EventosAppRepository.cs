using System.Linq.Expressions;
using EventosApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventosApp.Infrastructure.Repositories;

public class EventosAppRepository<T> : IRepository<T> where T : class, new()
{
    protected readonly EventosDbContext Context;
    private readonly DbSet<T> _entities;

    public EventosAppRepository(EventosDbContext context)
    {
        Context = context;
        _entities = Context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _entities.FindAsync(id) ?? new T();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate = null)
    {
        var query = _entities.AsNoTracking();
        var result = predicate is null 
            ? query.ToListAsync()
            : query.Where(predicate).ToListAsync();
        
        return await result.ConfigureAwait(false);
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }
}