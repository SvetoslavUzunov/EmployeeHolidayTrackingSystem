using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Repository;
using EmployeeHolidayTrackingSystem.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeHolidayTrackingSystem.Service;

public static class ServiceInjection
{
   public static IServiceCollection AddServices(this IServiceCollection services)
   {
      RepositoryInjection.AddRepositories(services);

      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IHolidayRequestService, HolidayRequestService>();

      services.AddScoped<ITokenHandlerService, TokenHandlerService>();
      services.AddScoped<IAuthenticationService, AuthenticationService>();

      return services;
   }
}
