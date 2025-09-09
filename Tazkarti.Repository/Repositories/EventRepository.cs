using Microsoft.EntityFrameworkCore;
using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Repository.Data;

namespace Tazkarti.Repository.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext context;

    public EventRepository(AppDbContext context)
    {
        this.context = context;
    }
    
    public async Task<Event> GetEventWithTickets(int id)
    {
        var Event = context.Events
            .Include(e => e.Tickets)
            .FirstOrDefault(e => e.Id == id);
        return Event;
    }
}