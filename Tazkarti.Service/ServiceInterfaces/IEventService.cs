using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;

namespace Tazkarti.Service.ServiceInterfaces;

public interface IEventService
{
    Task<List<Event>> GetAllEventsByCategoryAsync(int categoryId);
    
    Task<bool> addEventAsync(Event newEvent);

    Task<bool> DeleteEventAsync(int id);
    
    void updateEvent(Event updatedEvent);
}