using Tazkarti.Core.Models;

namespace Tazkarti.Service.ServiceInterfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    
    Task AddCategoryAsync(Category newCategory);

    Task<bool> DeleteCategoryAsync(int id);
    
    void UpdateCategory(Category category);
}