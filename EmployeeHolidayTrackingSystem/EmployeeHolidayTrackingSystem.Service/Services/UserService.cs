using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Contracts.Patterns;
using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Exceptions;
using EmployeeHolidayTrackingSystem.Core.Models.User;
using EmployeeHolidayTrackingSystem.Data.Entities;

namespace EmployeeHolidayTrackingSystem.Service.Services;

public class UserService : IUserService
{
   private readonly IUserRepository userRepository;
   private readonly IUnitOfWork unitOfWork;

   public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
   {
      this.userRepository = userRepository;
      this.unitOfWork = unitOfWork;
   }

   public async Task<UserModel> GetByIdAsync(Guid id)
   {
      var user = await userRepository.GetByIdAsync(id);

      if (user == null)
      {
         throw new ItemNotFoundException();
      }

      return new UserModel
      {
         Id = user.Id,
         UserName = user.UserName,
         Email = user.Email,
         HolidayDaysRemaining = user.HolidayDaysRemaining,
         IsActive = user.IsActive
      };
   }

   public async Task<IEnumerable<UserModel>> GetAllAsync()
   {
      var users = await userRepository.GetAllAsync();

      if (!users.Any())
      {
         throw new EmptyCollectionException();
      }

      return users.Where(user => user.IsActive)
         .Select(u => new UserModel
      {
         Id = u.Id,
         UserName = u.UserName,
         Email = u.Email,
         HolidayDaysRemaining = u.HolidayDaysRemaining,
         IsActive = u.IsActive,
         SupervisorEmailAddress = u.SupervisorEmailAddress
      })
      .ToList();
   }

   public async Task<UserModel> CreateAsync(UserModel userModel)
   {
      var user = await userRepository.GetByIdAsync(userModel.Id);

      if (user != null)
      {
         throw new ItemAlreadyExistException();
      }

      user = new UserEntity
      {
         UserName = userModel.UserName,
         Email = userModel.Email,
         SupervisorEmailAddress = UserConstants.SupervisorUserName
      };

      await userRepository.CreateAsync(user);
      await unitOfWork.CompleteAsync();

      return userModel;
   }

   public async Task<UserModel> EditAsync(UserModel userModel)
   {
      var user = await userRepository.GetByIdAsync(userModel.Id);

      if (user == null)
      {
         throw new ItemNotFoundException();
      }

      user.Id = userModel.Id;
      user.HolidayDaysRemaining = userModel.HolidayDaysRemaining;

      userRepository.Edit(user);
      await unitOfWork.CompleteAsync();

      return userModel;
   }

   public async Task DeleteByIdAsync(Guid id)
   {
      var user = await userRepository.GetByIdAsync(id);

      if (user == null || !user.IsActive)
      {
         throw new ItemNotFoundException();
      }

      user.IsActive = false;
      await unitOfWork.CompleteAsync();
   }
}
