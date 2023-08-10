using Microsoft.AspNetCore.Identity;

namespace EmployeeHolidayTrackingSystem.Data.Entities;

public class RoleEntity : IdentityRole<Guid>
{
   public virtual ICollection<UserRolesEntity> UserRoles { get; set; } = new HashSet<UserRolesEntity>();
}
