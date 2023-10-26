using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Users.Models;

public class User : IdentityUser
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public int DNI { get; set; }

    [Timestamp]
    public DateTime CreatedUser { get; set; }
    [Timestamp]
    public DateTime UpdateUser { get; set; }
    public bool IsActive { get; set; } = true;


}
