using Tazkarti.Core.Models;
using Tazkarti.Core.RepositoryInterfaces;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class MatchService : IMatchService
{
    private readonly IGenericRepository<Match> repository;

    public MatchService(IGenericRepository<Match> repository)
    {
        this.repository = repository;
    }
    
    public async Task<List<Match>> GetAllMatchesAsync()
    {
        var matches = await repository.GetAllAsync();
        return matches.ToList();
    }

    public async Task<Match> GetMatchByIdAsync(int id)
    {
        var match = await repository.GetByIdAsync(id);
        return match;
    }
}