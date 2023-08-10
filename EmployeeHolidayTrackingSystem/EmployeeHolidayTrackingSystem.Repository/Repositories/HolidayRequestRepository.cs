using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Data;
using EmployeeHolidayTrackingSystem.Data.Entities;

namespace EmployeeHolidayTrackingSystem.Repository.Repositories;

public class HolidayRequestRepository : GenericRepository<HolidayRequestEntity>, IHolidayRequestRepository
{
   public HolidayRequestRepository(EmployeeHolidayTrackingSystemDBContext data) : base(data) { }
}
