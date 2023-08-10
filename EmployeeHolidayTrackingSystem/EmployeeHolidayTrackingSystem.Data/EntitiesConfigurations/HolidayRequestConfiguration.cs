using EmployeeHolidayTrackingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHolidayTrackingSystem.Data.EntitiesConfigurations;

public class HolidayRequestConfiguration : IEntityTypeConfiguration<HolidayRequestEntity>
{
   public void Configure(EntityTypeBuilder<HolidayRequestEntity> builder)
   {
      builder
         .HasOne(h => h.User)
         .WithMany(s => s.Requests)
         .HasForeignKey(s => s.UserId)
         .OnDelete(DeleteBehavior.Restrict);
   }
}