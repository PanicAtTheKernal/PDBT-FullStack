using System.Linq.Expressions;

namespace PDBT_CompleteStack.Repository.Interfaces;

public interface IGenericRepository<T> where T : class
{
    T GetById(int id);
    Task<T> GetByIdAsync(int id);
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    bool Any(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

    void Add(T entity);
    void AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);
    Task<bool> Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}