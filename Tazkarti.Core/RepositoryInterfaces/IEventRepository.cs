using Tazkarti.Core.Models;

namespace Tazkarti.Core.RepositoryInterfaces;

public interface IEventRepository
{
    Task<Event> GetEventWithTickets(int id);
}