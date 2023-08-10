using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static EmployeeHolidayTrackingSystem.Core.Constants.UserConstants;

namespace EmployeeHolidayTrackingSystem.Data.Entities;

public class UserEntity : IdentityUser<Guid>
{
   [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
   public string? FirstName { get; set; }

   [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
   public string? LastName { get; set; }

   [EmailAddress]
   public override string Email { get; set; }

   public int HolidayDaysRemaining { get; set; } = InitialPaidLeavesCount;

   public bool IsActive { get; set; } = true;

   public string? SupervisorEmailAddress { get; set; }

   public virtual ICollection<UserRolesEntity> UserRoles { get; set; } = new HashSet<UserRolesEntity>();

   public virtual ICollection<HolidayRequestEntity> Requests { get; set; } = new HashSet<HolidayRequestEntity>();
}
