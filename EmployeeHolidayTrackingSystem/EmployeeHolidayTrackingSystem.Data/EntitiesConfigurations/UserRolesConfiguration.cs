using EmployeeHolidayTrackingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHolidayTrackingSystem.Data.EntitiesConfigurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<UserRolesEntity>
{
   public void Configure(EntityTypeBuilder<UserRolesEntity> builder)
   {
      builder
         .HasKey(ur => new { ur.UserId, ur.RoleId });
   }
}
