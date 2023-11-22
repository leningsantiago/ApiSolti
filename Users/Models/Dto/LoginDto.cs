using System.ComponentModel.DataAnnotations;

namespace Users.Models.Dto;
public class LoginDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    /// <summary>
    /// Get or send email.
    /// </summary>
    /// <example>example@email.com</example>
    public string Email { get; set; }

    [Required]
    /// <summary>
    /// Get or send password.
    /// </summary>
    /// <example>SuperPass123*</example>
    public string Password { get; set; }
}
