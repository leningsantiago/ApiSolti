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
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="registerDto">User data for registration.</param>
    /// <returns>Details of the newly created user.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/User
    ///     {
    ///         "firstName": "Juan",
    ///         "lastName": "Perez",
    ///         "dni": 1234567890
    ///         "email": "juan.perez@email.com",
    ///         "password": "SuperPass123*"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created user.</response>
    /// <response code="400">If there is an error in the request, returns error details.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Detail), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves a list of all users in the system.
    /// </summary>
    /// <returns>A list of users with their details.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/User/users
    ///
    /// </remarks>
    /// <response code="200">Returns the list of users.</response>
    /// <response code="400">If there is an error, returns error details.</response>
    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Detail), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        if (users.IsSuccessful)
        {
            return Ok(users);
        }
        else
        {
            return BadRequest(users);
        }
    }
    /// <summary>
    /// Finds a user in the system by their full name.
    /// </summary>
    /// <param name="fullName">Full name of the user to find.</param>
    /// <returns>Details of the found user.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/User/find-user?fullName=Juan Perez
    ///
    /// </remarks>
    /// <response code="200">Returns the details of the found user.</response>
    /// <response code="404">If the user is not found.</response>
    [HttpGet("find-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Detail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindUsersByName(string fullName)
    {
        var findUser = await _userService.FindUsersByName(fullName);
        if (findUser == null)
        {
            return NotFound();
        }
        return Ok(findUser);
    }
    /// <summary>
    /// Generates a password reset token for a user based on their email address.
    /// </summary>
    /// <param name="email">The email address of the user to generate a reset token for.</param>
    /// <returns>A response with the password reset token or an error message.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/User/password-reset-token
    ///     {
    ///         "email": "user@example.com"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the password reset token for the user.</response>
    /// <response code="404">If the user is not found or an error occurs.</response>
    [HttpPost("password-reset-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Detail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PasswordResetToken(string email)
    {
        var tokenPassword = await _userService.GeneratePasswordResetToken(email);
        if (tokenPassword.IsSuccessful)
        {
            return Ok(tokenPassword);
        }
        return NotFound(tokenPassword);
    }
    [HttpPost]
    public async Task<IActionResult> PasswordReset(ResetPasswordDto newPassword)
    {
        var passwordReset = await _userService.ResetPassword(newPassword);
        if(!passwordReset.IsSuccessful)
        {
            if(passwordReset.Status == ResponseStatus.BadRequest)
            {
                return BadRequest(passwordReset);
            }
            else if(passwordReset.Status == ResponseStatus.NotFound)
            {
                return NotFound(passwordReset);
            }
            else
            {
                return Unauthorized(passwordReset);
            }
        }
        return Ok(passwordReset);
    }
}
