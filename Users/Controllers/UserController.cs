using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Models.Dto;
using Users.Services.Interfaces;

namespace Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterDto newUser)
    {
        Detail response = await _userService.CreateUser(newUser);
        if (!response.IsSuccessful)
        {
            if (response.Status == ResponseStatus.Conflict) {
                return Conflict(response);
            }
            return BadRequest(response);
        }
        return Created("", response);
    }
}
