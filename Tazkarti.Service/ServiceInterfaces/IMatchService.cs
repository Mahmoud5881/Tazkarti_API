using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;

namespace Tazkarti.Service.ServiceInterfaces;

public interface IMatchService
{
    Task<List<Match>> GetAllMatchesAsync();
    Task<Match> GetMatchByIdAsync(int id);
}