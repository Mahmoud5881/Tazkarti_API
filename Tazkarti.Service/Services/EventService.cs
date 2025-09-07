using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class EventService : IEventService
{
    private readonly IGenericRepository<Event> repository;

    public EventService(IGenericRepository<Event> repository)
    {
        this.repository = repository;
    }

    public async Task<List<Event>> GetAllEventsByCategoryAsync(int categoryId)
    {
        var events = await repository.GetAllAsync();
        var categoryEvents = events.Where(e=>e.CategoryId == categoryId).ToList();
        return categoryEvents;
    }
}