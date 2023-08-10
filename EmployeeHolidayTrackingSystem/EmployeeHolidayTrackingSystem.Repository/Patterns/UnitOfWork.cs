using EmployeeHolidayTrackingSystem.Core.Contracts.Patterns;
using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Data;

namespace EmployeeHolidayTrackingSystem.Repository.Patterns;

public class UnitOfWork : IUnitOfWork
{
   private readonly EmployeeHolidayTrackingSystemDBContext data;

   public UnitOfWork(EmployeeHolidayTrackingSystemDBContext data,
      IUserRepository users,
      IHolidayRequestRepository holidays)
   {
      this.data = data;
      this.Users = users;
      this.Holidays = holidays;
   }

   public IUserRepository Users { get; }

   public IHolidayRequestRepository Holidays { get; }

   public async Task<int> CompleteAsync()
      => await data.SaveChangesAsync();

   public void Dispose()
   {
      Dispose(true);
      GC.SuppressFinalize(this);
   }

   protected virtual void Dispose(bool disposing)
   {
      if (disposing)
      {
         data.Dispose();
      }
   }
}
