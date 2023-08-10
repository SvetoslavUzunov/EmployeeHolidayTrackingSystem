using EmployeeHolidayTrackingSystem.Core.Contracts.Patterns;
using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Data;
using EmployeeHolidayTrackingSystem.Repository.Patterns;
using EmployeeHolidayTrackingSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeHolidayTrackingSystem.Repository;

public static class RepositoryInjection
{
   public static IServiceCollection AddRepositories(this IServiceCollection services)
   {
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IHolidayRequestRepository, HolidayRequestRepository>();

      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddDbContext<EmployeeHolidayTrackingSystemDBContext>(options => options
            .UseSqlServer(new DBConnectionString().GetDbConnectionString));

      return services;
   }
}
