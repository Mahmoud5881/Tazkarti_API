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

    public async Task AddMatchAsync(Match match) =>
        await repository.CreateAsync(match);

    public async Task<bool> DeleteMatchAsync(int id)
    {
        Match match = await repository.GetByIdAsync(id);
        if (match != null)
        {
            await repository.DeleteAsync(id);
            return true;
        }
        return false;
    }

    public async void UpdateMatch(Match match) =>
        repository.Update(match);
}