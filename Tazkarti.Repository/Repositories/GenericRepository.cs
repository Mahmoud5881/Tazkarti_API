using Microsoft.EntityFrameworkCore;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Repository.Data;

namespace Tazkarti.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext context;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await context.Set<T>().ToListAsync();
        return result;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var result = await context.Set<T>().FindAsync(id);
        return result;
    }

    public async Task DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);
        context.Set<T>().Remove(result);
        await context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }

    public async Task CreateAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }
}