using System.Text.Json.Serialization;
using Users.Migrations;

namespace Users.Models.Dto;

public class Detail
{
    [JsonIgnore]
    public ResponseStatus Status { get; set; }
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public string? Token { get; set; }
    public UserDto? User { get; set; }
    public List<string>? Errors { get; set; }
}
public class UserDto
{
    /// <summary>
    /// Get or send UserId.
    /// </summary>
    /// <example>f8c1b26b-c23f-4788-962b-6158e06e6426</example>
    public string UserId { get; set; }
    /// <summary>
    /// Get or send first name.
    /// </summary>
    /// <example>Juan</example>
    public string FirstName { get; set; }
    /// <summary>
    /// Get or send last name.
    /// </summary>
    /// <example>Perez</example>
    public string LastName { get; set; }
    /// <summary>
    /// Get or send email.
    /// </summary>
    /// <example>Petito@example.com</example>
    public string Email { get; set; }
    /// <summary>
    /// Get or send DNI.
    /// </summary>
    /// <example>1234567890</example>
    public int? DNI { get; set; }
}
public enum ResponseStatus : int
{
    Success = 200,              // OK
    Created = 201,              // created
    NoContent = 204,            // No Content
    BadRequest = 400,           // Bad Request
    Unauthorized = 401,         // Unauthorized
    NotFound = 404,             // Not Found
    Conflict = 409,             // Conflict 
    InternalServerError = 500   // Internal Server Error
}
