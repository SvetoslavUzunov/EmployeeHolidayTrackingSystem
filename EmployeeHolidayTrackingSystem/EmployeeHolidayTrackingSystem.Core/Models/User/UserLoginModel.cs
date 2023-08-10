using System.ComponentModel.DataAnnotations;

namespace EmployeeHolidayTrackingSystem.Core.Models.User;

public class UserLoginModel
{
   public string? UserName { get; set; }

   [Required(ErrorMessage = "Email is required!")]
   public string Email { get; set; }

   [Required(ErrorMessage = "Password is required!")]
   public string Password { get; set; }
}
