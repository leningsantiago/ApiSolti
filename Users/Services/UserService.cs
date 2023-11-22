using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Users.Data;
using Users.Models;
using Users.Models.Dto;
using Users.Services.Interfaces;

namespace Users.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly UserDbContext _userDbContext;

    public UserService(UserManager<User> userManager, UserDbContext userDbContext)
    {
        _userManager = userManager;
        _userDbContext = userDbContext;
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
            FirstName = newUser.FirstName.ToUpper(),
            LastName = newUser.LastName.ToUpper(),
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
    public async Task<Detail> GetAllUsers()
    {
        var users = await _userDbContext.Users
            .Select(user => ItemUserDto(user))
            .ToListAsync();
        if (users.IsNullOrEmpty())
        {
            return new Detail
            {
                IsSuccessful = false,
                Status = ResponseStatus.NotFound,
                Message = "User not found",
            };
        }
        return new Detail
        {
            IsSuccessful = true,
            Status = ResponseStatus.Success,
            Message = "User found",
            Users = users
        };
    }
    public async Task<Detail> FindUsersByName(string name)
    {
        var normalizedName = name.ToLower();
        var foundUsers = await _userDbContext.Users
            .Where(u => (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(normalizedName))
            .Select(user => ItemUserDto(user))
            .ToListAsync();

        if (foundUsers.IsNullOrEmpty())
        {
            return new Detail
            {
                IsSuccessful = false,
                Status = ResponseStatus.NotFound,
                Message = "Users not found",
            };
        }
        return new Detail
        {
            IsSuccessful = true,
            Status = ResponseStatus.Success,
            Message = "Users found",
            Users = foundUsers
        };
    }
    public async Task<Detail> FindUserByEmail(string email)
    {
        User user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new Detail
            {
                Status = ResponseStatus.NotFound,
                IsSuccessful = false,
                Message = "User not found."
            };
        }
        return new Detail
        {
            Status = ResponseStatus.Success,
            IsSuccessful = true,
            Message = "User found",
            User = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email,
                DNI = user.DNI
            },
        };
    }
    public async Task<Detail> GeneratePasswordResetToken(string email)
    {
        User findUser = await _userManager.FindByEmailAsync(email);
        if (findUser == null)
        {
            return new Detail
            {
                Status = ResponseStatus.NotFound,
                IsSuccessful = false,
                Message = "User not found."
            };
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(findUser);
        return new Detail
        {
            Status = ResponseStatus.Success,
            IsSuccessful = true,
            Message = "Password reset token generated.",
            Token = token
        };
    }
    public async Task<Detail> ResetPassword(ResetPasswordDto passwordDto)
    {
        User user = await _userManager.FindByEmailAsync(passwordDto.Email);
        if (user == null)
        {
            return new Detail
            {
                Status = ResponseStatus.NotFound,
                IsSuccessful = false,
                Message = "User not found."
            };
        }
        if(!(passwordDto.Password == passwordDto.PasswordRepeat))
        {
            return new Detail
            {
                Status = ResponseStatus.Unauthorized,
                IsSuccessful = false,
                Message = "Password no equals"
            };
        }
        var resetPassword = await _userManager.ResetPasswordAsync(user, passwordDto.Token, passwordDto.Password);
        if (!resetPassword.Succeeded)
        {
            return new Detail
            {
                Status = ResponseStatus.BadRequest,
                IsSuccessful = false,
                Message = "Error resetting password.",
                Errors = resetPassword.Errors.Select(e => e.Description).ToList()
            };
        }
        return new Detail
        {
            Status = ResponseStatus.Success,
            IsSuccessful = true,
            Message = "Password has been reset successfully."
        };
    }
    private static UserDto ItemUserDto(User user) =>
        new UserDto
        {
            Id = user.Id.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            DNI = user.DNI
        };
}

