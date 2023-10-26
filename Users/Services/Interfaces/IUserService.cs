using Users.Models.Dto;

namespace Users.Services.Interfaces;

public interface IUserService
{
    Task<Detail> CreateUser(RegisterDto newUser);
}
