using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> categoryRepository;

    public CategoryService(IGenericRepository<Category> categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.ToList();
    }

    public async Task AddCategoryAsync(Category newCategory) => 
        await categoryRepository.CreateAsync(newCategory);

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category != null)
        {
            await categoryRepository.DeleteAsync(id);
            return true;
        }
        return false;
    }
    
    public void UpdateCategory(Category updatedCategory) => 
        categoryRepository.Update(updatedCategory);
}