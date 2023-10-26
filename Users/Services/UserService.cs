using Microsoft.AspNetCore.Identity;
using Users.Models;
using Users.Models.Dto;
using Users.Services.Interfaces;

namespace Users.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Detail> CreateUser(RegisterDto newUser)
    {
        User findUser =  await _userManager.FindByEmailAsync(newUser.Email);
        if (findUser != null) {
            return new Detail
            {
                Status = ResponseStatus.Conflict,
                IsSuccessful = false,
                Message = "User already exists"
            };
        }
        User createUser = new User
        {
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            UserName = newUser.Email,
            Email = newUser.Email,
            DNI = newUser.DNI,
            CreatedUser = DateTime.Now,
            EmailConfirmed = true,
        };
        var result = await _userManager.CreateAsync(createUser,newUser.Password);
        if (!result.Succeeded)
        {
            return new Detail
            {
                Status = ResponseStatus.BadRequest,
                IsSuccessful = false,
                Message = "Error registering the user",
                Errors = result.Errors.Select(x => x.Description).ToList()
            };
        }
        await _userManager.AddToRoleAsync(createUser, "User");
        return new Detail
        {
            Status = ResponseStatus.Created,
            IsSuccessful = true,
            Message = "User Successfully Created",
        };
    }
}
