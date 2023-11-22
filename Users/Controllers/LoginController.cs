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
    /// <summary>
    /// Log a user into the system.
    /// </summary>
    /// <param name="loginDto">User data to obtain an authorization token.</param>
    /// <returns>Details of the login attempt, including user data if successful.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/User/Login
    ///     {
    ///         "email": "example@aliadascargo.com.co",
    ///         "password": "SuperPass123*"
    ///     }
    /// </remarks>
    /// <response code="200">Returns user details if login is successful.</response>
    /// <response code="401">If the login attempt is unauthorized, returns error details.</response>
    /// <response code="404">If the user is not found, returns error details.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Detail), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Detail), StatusCodes.Status404NotFound)]
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
