using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> repository;

    public CategoryService(IGenericRepository<Category> repository)
    {
        this.repository = repository;
    }
    
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await repository.GetAllAsync();
        return categories.ToList();
    }
}