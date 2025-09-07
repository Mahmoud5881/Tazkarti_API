namespace Tazkarti.Core.RepositoryInterfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
    
    void UpdateAsync(T entity);
    
    Task CreateAsync(T entity);
}