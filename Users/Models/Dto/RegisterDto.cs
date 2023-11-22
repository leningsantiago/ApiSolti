using System.ComponentModel.DataAnnotations;

namespace Users.Models.Dto;

public class RegisterDto
{
    /// <summary>
    /// Get or send first name.
    /// </summary>
    /// <example>Juan</example>
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚÑñüÜ\s]{1,50}$",
         ErrorMessage = "Only alphabetic characters and spaces are allowed, with a maximum length of 50")]
    public string FirstName { get; set; }

    /// <summary>
    /// Get or send last name.
    /// </summary>
    /// <example>Perez</example>
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚÑñüÜ\s]{1,50}$",
         ErrorMessage = "Only alphabetic characters and spaces are allowed, with a maximum length of 50")]
    public string LastName { get; set; }

    /// <summary>
    /// Get or send email.
    /// </summary>
    /// <example>juan.perez@aliadascargo.com.co</example>
    [Required]
    [MaxLength(50, ErrorMessage = "The maximum length of the email is 50 characters.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@aliadascargo\.com\.co$", ErrorMessage = "Only emails with domain '@aliadascargo.com.co' are allowed.")]
    public string Email { get; set; }

    /// <summary>
    /// Get or send password.
    /// </summary>
    /// <example>SuperPass123*</example>
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$",
     ErrorMessage = "The password must have at least one uppercase letter, one lowercase letter, one number, and one special character (@, $, !, %, *, ?, &, or #).")]
    public string Password { get; set; }

    /// <summary>
    /// Get or send first name.
    /// </summary>
    /// <example>123456789</example>
    [Required]
    [RegularExpression(@"^\d{7,10}$",
         ErrorMessage = "Only numeric max digits 10 ")]
    public int DNI { get; set; }
}
