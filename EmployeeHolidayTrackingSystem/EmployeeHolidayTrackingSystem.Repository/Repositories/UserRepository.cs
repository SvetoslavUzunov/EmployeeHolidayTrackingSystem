using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Data;
using EmployeeHolidayTrackingSystem.Data.Entities;

namespace EmployeeHolidayTrackingSystem.Repository.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
   public UserRepository(EmployeeHolidayTrackingSystemDBContext data) : base(data) { }

}
