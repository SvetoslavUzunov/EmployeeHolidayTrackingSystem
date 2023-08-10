using System.ComponentModel.DataAnnotations;
using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Models.User;

namespace EmployeeHolidayTrackingSystem.Core.Models.Role
{
   public class RoleModel
   {
      public Guid Id { get; set; }=Guid.NewGuid();

      [Required]
      [StringLength(RoleConstants.NameMaxLength, MinimumLength = RoleConstants.NameMinLength)]
      public string Name { get; set; }

      public virtual ICollection<UserModel>? Users { get; set; } = new HashSet<UserModel>();
   }
}
