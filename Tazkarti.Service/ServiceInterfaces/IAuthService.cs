using System.IdentityModel.Tokens.Jwt;
using Tazkarti.Core.Models;

namespace Tazkarti.Service.ServiceInterfaces;

public interface IAuthService
{
    Task<JwtSecurityToken> CreateTokenAsync(AppUser user);
}