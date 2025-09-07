using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
using Tazkarti.Core.Models;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.API.Controllers;
[Route("api/matches")]
[ApiController]
public class MatchController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMatchService matchService;

    public MatchController(IMatchService matchService, IMapper mapper)
    {
        this.mapper = mapper;
        this.matchService = matchService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllMatches()
    {
        var allMatches = await matchService.GetAllMatchesAsync();
        var matches = mapper.Map<IEnumerable<Match>,IEnumerable<MatchDTO>>(allMatches);
        if(matches != null && matches.Any())
            return Ok(matches);
        return NotFound("No data to display");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatchById(int id)
    {
        var result = await matchService.GetMatchByIdAsync(id);
        var match = mapper.Map<Match,MatchDTO>(result);
        if(match != null)
            return Ok(match);
        return NotFound("No data to display");
    }
}
