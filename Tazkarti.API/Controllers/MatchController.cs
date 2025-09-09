using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
using Tazkarti.API.Helpers;
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
    [ResponseCache(Duration = 60 * 5)]
    public async Task<IActionResult> GetAllMatches([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var allMatches = await matchService.GetMatchesWithPaginationAsync(pageIndex, pageSize);
        if (allMatches != null && allMatches.Any())
        {
            var matches = mapper.Map<IEnumerable<Match>,IEnumerable<MatchToReturnDTO>>(allMatches);
            var count = allMatches.Count();
            return Ok(new Pagination<MatchToReturnDTO>(pageIndex, pageSize, count, matches));
        }

        return NotFound("No data to display");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatchById(int id)
    {
        var result = await matchService.GetMatchByIdAsync(id);
        var match = mapper.Map<Match,MatchToReturnDTO>(result);
        if(match != null)
            return Ok(match);
        return NotFound("No data to display");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostMatch(MatchDTO newMatch)
    {
        if (ModelState.IsValid)
        {
            var match = mapper.Map<MatchDTO, Match>(newMatch);
            await matchService.AddMatchAsync(match);
            return Created();
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        var result = await matchService.DeleteMatchAsync(id);
        if(result)
            return NoContent();
        return NotFound("Match not found");
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutMatch(MatchDTO updatedMatch)
    {
        if (ModelState.IsValid)
        {
            var match = mapper.Map<MatchDTO, Match>(updatedMatch);
            matchService.UpdateMatch(match);
            var matchToReturn = mapper.Map<Match, MatchToReturnDTO>(match);
            return Ok(matchToReturn);
        }
        return BadRequest(ModelState);
    }
}
