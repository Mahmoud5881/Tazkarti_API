using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class EventService : IEventService
{
    private readonly IGenericRepository<Event> repository;
    private readonly IGenericRepository<Category> categoryRepository;
    private readonly IEventRepository eventRepository;

    public EventService(IGenericRepository<Event> repository, IGenericRepository<Category> categoryRepository, IEventRepository eventRepository)
    {
        this.repository = repository;
        this.categoryRepository = categoryRepository;
        this.eventRepository = eventRepository;
    }

    public async Task<List<Event>> GetAllEventsByCategoryAsync(int categoryId)
    {
        var events = await repository.GetAllAsync();
        var categoryEvents = events.Where(e=>e.CategoryId == categoryId).ToList();
        return categoryEvents;
    }

    public async Task<bool> addEventAsync(Event newEvent)
    {
        var category = categoryRepository.GetByIdAsync(newEvent.CategoryId);
        if (newEvent != null && category != null)
        {
            await repository.CreateAsync(newEvent);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var Event = await repository.GetByIdAsync(id);
        if (Event != null)
        {
            await repository.DeleteAsync(id);
            return true;
        }
        return false;
    }

    public void updateEvent(Event newEvent)  => repository.Update(newEvent);

    public async Task<Event> GetEventWithTicketsAsync(int id)
    {
        var Event = await eventRepository.GetEventWithTickets(id);
        return Event;
    }
}