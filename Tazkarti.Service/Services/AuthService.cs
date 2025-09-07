using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tazkarti.Core.Models;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.Service.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;

    public AuthService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public async Task<JwtSecurityToken> CreateTokenAsync(AppUser user)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: configuration["Jwt:ValidIssuer"],
            audience: configuration["Jwt:ValidAudience"],
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: creds
        );
        return token;
    }
}