using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
using Tazkarti.API.Helpers;
using Tazkarti.Core.Models;
using Tazkarti.Service.ServiceInterfaces;
using Tazkarti.Service.Services;

namespace Tazkarti.API.Controllers;
[Route("api/[controller]")]
[ApiController] 
public class EventController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IEventService eventService;
    private readonly ICategoryService categoryService;

    public EventController(IEventService eventService, ICategoryService categoryService, IMapper mapper)
    {
        this.eventService = eventService;
        this.categoryService = categoryService;
        this.mapper = mapper;
    }
    
    [HttpGet("Categories")]
    [ResponseCache(Duration = 60 * 5)]
    public async Task<IActionResult> GetAllCategories([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var allCategories = await categoryService.GetCategoriesWithPaginationAsync(pageIndex, pageSize);
        if (allCategories != null && allCategories.Any())
        {
            var categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryToReturnDTO>>(allCategories);
            var count = allCategories.Count();
            return Ok(new Pagination<CategoryToReturnDTO>(pageIndex, pageSize, count, categories));
        }
        
        return NotFound("No data to display");
    }

    [HttpGet("Category/{id}")]
    [ResponseCache(Duration = 60 * 5)]
    public async Task<IActionResult> GetEventsByCategory(int categoryId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var allEvents = await eventService.GetEventsWithPaginationAsync(categoryId, pageIndex, pageSize);
        if (allEvents != null && allEvents.Any())
        {
            var events = mapper.Map<IEnumerable<Event>, IEnumerable<EventToReturnDTO>>(allEvents);
            var count = allEvents.Count();
            return Ok(new Pagination<EventToReturnDTO>(pageIndex, pageSize, count, events));
        }

        return NotFound("No data to display");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostEvent(EventDTO newEvent)
    {
        if (ModelState.IsValid)
        {
            var Event = mapper.Map<EventDTO, Event>(newEvent);
            var result  = await eventService.addEventAsync(Event);
            if (result)
            {
                var eventToReturn = mapper.Map<Event, EventToReturnDTO>(Event);
                return Created("", eventToReturn);
            }
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var result = await eventService.DeleteEventAsync(id);
        if(result)
            return NoContent();
        return NotFound("Event not found");
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult PutEvent(EventDTO updatedEvent)
    {
        if (ModelState.IsValid)
        {
            var Event = mapper.Map<EventDTO, Event>(updatedEvent);
            eventService.updateEvent(Event);
            var eventToReturn = mapper.Map<Event, EventToReturnDTO>(Event);
            return Ok(eventToReturn);
        }
        return BadRequest(ModelState);
    }
}