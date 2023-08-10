using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Models.Role;
using System.ComponentModel.DataAnnotations;

namespace EmployeeHolidayTrackingSystem.Core.Models.User;

public class UserModel
{
   public Guid Id { get; set; }= Guid.NewGuid();

   [StringLength(UserConstants.UserNameMaxLength, MinimumLength = UserConstants.UserNameMinLength)]
   public string? UserName { get; set; }

   [Required]
   [EmailAddress]
   public string Email { get; set; }

   public bool IsActive { get; set; } = true;

   public int HolidayDaysRemaining { get; set; }

   public string? SupervisorEmailAddress { get; set; }

   //[Required]
   //[StringLength(UserConstants.FirstNameMaxLength, MinimumLength = UserConstants.FirstNameMinLength)]
   //public string? FirstName { get; set; }

   //[Required]
   //[StringLength(UserConstants.LastNameMaxLength, MinimumLength = UserConstants.LastNameMinLength)]
   //public string? LastName { get; set; }

   //public int RoleId { get; set; }

   //public virtual RoleModel Role { get; set; }
}
