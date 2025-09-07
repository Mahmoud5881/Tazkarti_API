using System.Collections;
using AutoMapper;
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
        var categories = mapper.Map<IEnumerable<Category>,IEnumerable<CategoryDTO>>(allCategories);
        if(categories != null && categories.Any())
            return Ok(categories);
        return NotFound("No data to display");
    }

    [HttpGet("Category/{id}")]
    public async Task<IActionResult> GetEventsByCategory(int categoryId)
    {
        var allEvents = await eventService.GetAllEventsByCategoryAsync(categoryId);
        var events = mapper.Map<IEnumerable<Event>, IEnumerable<EventDTO>>(allEvents);
        if(events != null && events.Any())
            return Ok(events);
        return NotFound("No data to display");
    }
}