using EmployeeHolidayTrackingSystem.Data.Entities;
using EmployeeHolidayTrackingSystem.Data.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHolidayTrackingSystem.Data;

public class EmployeeHolidayTrackingSystemDBContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
   public EmployeeHolidayTrackingSystemDBContext(DbContextOptions<EmployeeHolidayTrackingSystemDBContext> options) : base(options) { }

   public override DbSet<UserEntity> Users { get; set; }

   public override DbSet<RoleEntity> Roles { get; set; }

   public DbSet<HolidayRequestEntity> HolidayRequests { get; set; }

   protected override void OnModelCreating(ModelBuilder builder)
   {
      builder.ApplyConfiguration(new UserRolesConfiguration());
      builder.ApplyConfiguration(new HolidayRequestConfiguration());

      base.OnModelCreating(builder);
   }
}
