using Microsoft.AspNetCore.Identity;
using Users.Models;
using Users.Models.Dto;
using Users.Services.Interfaces;

namespace Users.Services
{
    public class LoginService : ILoginServices
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public LoginService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Detail> Login(string userName, string password)
        {
            User findUser = await _userManager.FindByNameAsync(userName)??
                await _userManager.FindByEmailAsync(userName);

            if (findUser == null)
            {
                return new Detail {
                    Status = ResponseStatus.NotFound,
                    IsSuccessful = false,
                    Message = "Invalid credentials provided.",
                };
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(findUser, password);
            if (!isPasswordValid) { 
                return new Detail
                {
                    Status = ResponseStatus.Unauthorized,
                    IsSuccessful = false,
                    Message = "Invalid credentials provided.",
                };
            }
            var token = await _tokenService.CreateToken(findUser);
            return new Detail
            {
                Status = ResponseStatus.Success,
                IsSuccessful = true,
                Message = "User authenticated successfully",
                Token = token
            };
        }
    }
}
