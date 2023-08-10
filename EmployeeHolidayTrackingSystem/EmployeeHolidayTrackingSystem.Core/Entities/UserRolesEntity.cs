namespace EmployeeHolidayTrackingSystem.Data.Entities;

public class UserRolesEntity
{
   public Guid UserId { get; set; } = Guid.NewGuid();

   public virtual UserEntity User { get; set; }

   public Guid RoleId { get; set; } = Guid.NewGuid();

   public virtual RoleEntity Role { get; set; }
}