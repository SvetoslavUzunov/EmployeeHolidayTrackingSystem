using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Exceptions;
using EmployeeHolidayTrackingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeHolidayTrackingSystem.Data;

public static class SeedDataManager
{
   public static async Task<IHost> SeedDataAsync(this IHost host)
   {
      Guid UserAdminId = Guid.NewGuid();
      Guid UserSupervisorId = Guid.NewGuid();

      using (IServiceScope? scope = host.Services.CreateScope())
      {
         try
         {
            using EmployeeHolidayTrackingSystemDBContext? data = scope.ServiceProvider.GetRequiredService<EmployeeHolidayTrackingSystemDBContext>();

            RoleManager<RoleEntity> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
            UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

            await data.Database.MigrateAsync();

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager, UserAdminId, UserSupervisorId);
            await AddUsersToRolesAsync(userManager, UserAdminId, UserSupervisorId);

            await data.SaveChangesAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      return host;
   }

   private static async Task SeedRolesAsync(RoleManager<RoleEntity> roleManager)
   {
      var adminRole = RoleConstants.AdminRole;
      var supervisorRole = RoleConstants.SupervisorRole;
      var employeeRole = RoleConstants.EmployeeRole;

      var isAdminRoleExist = await roleManager.FindByNameAsync(adminRole);
      if (isAdminRoleExist == null)
      {
         var role = new RoleEntity()
         {
            Name = adminRole
         };

         var isRoleCreated = await roleManager.CreateAsync(role);

         if (!isRoleCreated.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isRoleCreated);
         }
      }

      var isSupervisorRoleExist = await roleManager.FindByNameAsync(supervisorRole);
      if (isSupervisorRoleExist == null)
      {
         var role = new RoleEntity
         {
            Name = supervisorRole
         };

         var isRoleCreated = await roleManager.CreateAsync(role);

         if (!isRoleCreated.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isRoleCreated);
         }
      }

      var isEmployeeRoleExist = await roleManager.FindByNameAsync(employeeRole);
      if (isEmployeeRoleExist == null)
      {
         var role = new RoleEntity
         {
            Name = employeeRole
         };

         var isRoleCreated = await roleManager.CreateAsync(role);

         if (!isRoleCreated.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isRoleCreated);
         }
      }
   }

   private static async Task SeedUsersAsync(UserManager<UserEntity> userManager, Guid userAdminId, Guid userSupervisorId)
   {
      var userAdminEmail = UserConstants.AdminUserEmail;
      var isAdminUserExist = await userManager.FindByEmailAsync(userAdminEmail);

      if (isAdminUserExist == null)
      {
         var user = new UserEntity()
         {
            Id = userAdminId,
            UserName = UserConstants.AdminUserName,
            Email = userAdminEmail
         };

         var isUserCreated = await userManager.CreateAsync(user, UserConstants.AdminUserPassword);

         if (!isUserCreated.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isUserCreated);
         }
      }

      var userSupervisorEmail = UserConstants.SupervisorUserEmail;
      var isSupervisorUserExist = await userManager.FindByEmailAsync(userSupervisorEmail);

      if (isSupervisorUserExist == null)
      {
         var user = new UserEntity()
         {
            Id = userSupervisorId,
            UserName = UserConstants.SupervisorUserName,
            Email = userSupervisorEmail
         };

         var isUserCreated = await userManager.CreateAsync(user, UserConstants.SupervisorUserPassword);

         if (!isUserCreated.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isUserCreated);
         }
      }
   }

   private static async Task AddUsersToRolesAsync(UserManager<UserEntity> userManager, Guid userAdminId, Guid userSupervisorId)
   {
      var adminRole = RoleConstants.AdminRole;
      var adminUser = await userManager.FindByIdAsync(userAdminId.ToString());

      if (adminUser != null)
      {
         var isUserIsInRole = await userManager.IsInRoleAsync(adminUser, adminRole);

         if (isUserIsInRole)
         {
            throw new SeedDataException();
         }

         var isUserAddedToRole = await userManager.AddToRoleAsync(adminUser, adminRole);

         if (!isUserAddedToRole.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isUserAddedToRole);
         }
      }

      var supervisorRole = RoleConstants.SupervisorRole;
      var supervisorUser = await userManager.FindByIdAsync(userSupervisorId.ToString());

      if (supervisorUser != null)
      {
         var isUserIsInRole = await userManager.IsInRoleAsync(supervisorUser, supervisorRole);

         if (isUserIsInRole)
         {
            throw new SeedDataException();
         }

         var isUserAddedToRole = await userManager.AddToRoleAsync(supervisorUser, supervisorRole);

         if (!isUserAddedToRole.Succeeded)
         {
            ErrorHandler.ExecuteErrorHandler(isUserAddedToRole);
         }
      }
   }
}
