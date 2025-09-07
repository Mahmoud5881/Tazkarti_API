using Tazkarti.Core.Models;

namespace Tazkarti.Service.ServiceInterfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
}