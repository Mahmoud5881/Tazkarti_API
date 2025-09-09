using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.API.DTOs;
using Tazkarti.Core.Models;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthService authService;

        public AccountController(UserManager<AppUser> userManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO newUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = newUser.Username,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,  
                    Email = newUser.Email,
                    PhoneNumber = newUser.PhoneNumber,
                    BirthDate = newUser.BirthDate,
                    City = newUser.City,
                    Region = newUser.Region,
                    Language = newUser.Language
                };
                
                IdentityResult result = await userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    JwtSecurityToken token = await authService.CreateTokenAsync(user);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            return BadRequest();
        }
    }
}
