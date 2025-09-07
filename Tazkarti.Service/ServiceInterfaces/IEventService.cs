using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;

namespace Tazkarti.Service.ServiceInterfaces;

public interface IEventService
{
    Task<List<Event>> GetAllEventsByCategoryAsync(int categoryId);
}