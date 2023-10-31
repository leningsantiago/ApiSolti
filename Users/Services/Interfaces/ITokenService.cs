using Users.Models;

namespace Users.Services.Interfaces;
public interface ITokenService
{
    Task<string> CreateToken(User user);
}
