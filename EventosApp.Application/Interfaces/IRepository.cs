using System.Linq.Expressions;

namespace EventosApp.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(); 
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate = null);
    Task AddAsync(T entity); 
    void Update(T entity);
    void Delete(T entity);
}