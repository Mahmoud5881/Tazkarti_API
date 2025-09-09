using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
using Tazkarti.Core.Models;

namespace Tazkarti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDTO newRole)
        {
            if (ModelState.IsValid)
            {
                var Role = new IdentityRole(newRole.Name);
                IdentityResult result = await roleManager.CreateAsync(Role);
                if (result.Succeeded)
                    return Created();
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> AddToRole([FromQuery]string userName,[FromQuery] string role)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await userManager.AddToRoleAsync(user, role);
                if(result.Succeeded)
                    return Ok();
            }
            return BadRequest("User or Role not found");
        }
    }
}
