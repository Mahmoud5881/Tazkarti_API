using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
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
    public async Task<IActionResult> GetAllCategories()
    {
        var allCategories = await categoryService.GetAllCategoriesAsync();
        var categories = mapper.Map<IEnumerable<Category>,IEnumerable<CategoryToReturnDTO>>(allCategories);
        if(categories != null && categories.Any())
            return Ok(categories);
        return NotFound("No data to display");
    }

    [HttpGet("Category/{id}")]
    public async Task<IActionResult> GetEventsByCategory(int categoryId)
    {
        var allEvents = await eventService.GetAllEventsByCategoryAsync(categoryId);
        var events = mapper.Map<IEnumerable<Event>, IEnumerable<EventToReturnDTO>>(allEvents);
        if(events != null && events.Any())
            return Ok(events);
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
            if(result)
                return Created();
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