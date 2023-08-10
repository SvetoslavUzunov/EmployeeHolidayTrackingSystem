using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;

namespace EmployeeHolidayTrackingSystem.Core.Contracts.Patterns;

public interface IUnitOfWork : IDisposable
{
   public IUserRepository Users { get; }

   public IHolidayRequestRepository Holidays { get; }

   public Task<int> CompleteAsync();
}
