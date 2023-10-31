using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Users.Models.Dto;
using Users.Services.Interfaces;

namespace Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginServices _loginServices;
    public LoginController(ILoginServices loginServices)
    {
        _loginServices = loginServices;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var response = await _loginServices.Login(loginDto.Email, loginDto.Password);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        if (response.Status == ResponseStatus.NotFound)
        {
            return NotFound(response);
        }
        return Unauthorized(response);
    }
}
