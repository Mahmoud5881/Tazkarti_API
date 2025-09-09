using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class EventService : IEventService
{
    private readonly IGenericRepository<Event> eventRepository;
    private readonly IGenericRepository<Category> categoryRepository;

    public EventService(IGenericRepository<Event> eventRepository, IGenericRepository<Category> categoryRepository)
    {
        this.eventRepository = eventRepository;
        this.categoryRepository = categoryRepository;
    }

    public async Task<List<Event>> GetAllEventsByCategoryAsync(int categoryId)
    {
        var events = await eventRepository.GetAllAsync();
        var categoryEvents = events.Where(e=>e.CategoryId == categoryId).ToList();
        return categoryEvents;
    }

    public async Task<bool> addEventAsync(Event newEvent)
    {
        var category = categoryRepository.GetByIdAsync(newEvent.CategoryId);
        if (newEvent != null && category != null)
        {
            await eventRepository.CreateAsync(newEvent);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var Event = await eventRepository.GetByIdAsync(id);
        if (Event != null)
        {
            await eventRepository.DeleteAsync(id);
            return true;
        }
        return false;
    }

    public void updateEvent(Event newEvent)  => eventRepository.Update(newEvent);
}