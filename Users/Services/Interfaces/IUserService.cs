using Users.Models.Dto;

namespace Users.Services.Interfaces;

public interface IUserService
{
    Task<Detail> CreateUser(RegisterDto newUser);
    Task<Detail> GetAllUsers();
    Task<Detail> FindUsersByName(string name);
    Task<Detail> FindUserByEmail(string email);
    Task<Detail> GeneratePasswordResetToken(string email);
    Task<Detail> ResetPassword(ResetPasswordDto passwordDto);
}
