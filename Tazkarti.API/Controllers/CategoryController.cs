using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
using Tazkarti.Core.Models;
using Tazkarti.Service.ServiceInterfaces;
using Tazkarti.Service.Services;

namespace Tazkarti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDTO newCategory)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<CategoryDTO, Category>(newCategory);
                await categoryService.AddCategoryAsync(category);
                return Created();
            }
            return BadRequest(ModelState);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);
            if(result)
                return NoContent();
            return NotFound();
        }
        
        [HttpPut]
        public IActionResult PutCategory(CategoryDTO newCategory)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<CategoryDTO, Category>(newCategory);
                categoryService.UpdateCategory(category);
                var categoryToReturn = mapper.Map<Category, CategoryDTO>(category);
                return Ok(categoryToReturn);
            }
            return BadRequest(ModelState);
        }
    }
}
