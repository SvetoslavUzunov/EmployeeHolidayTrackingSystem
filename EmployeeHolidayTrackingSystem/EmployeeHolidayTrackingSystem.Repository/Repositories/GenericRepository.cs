using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHolidayTrackingSystem.Repository.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
   private readonly EmployeeHolidayTrackingSystemDBContext data;
   private readonly DbSet<TEntity> table;

   protected GenericRepository(EmployeeHolidayTrackingSystemDBContext data)
   {
      this.data = data;
      this.table = data.Set<TEntity>();
   }

   public virtual async Task<TEntity> GetByIdAsync(Guid id)
      => await table.FindAsync(id);

   public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
      => await table.ToListAsync();

   public virtual async Task CreateAsync(TEntity item)
      => await table.AddAsync(item);

   public virtual void Edit(TEntity item)
      => data.Entry(item).State = EntityState.Modified;

   public virtual async Task DeleteByIdAsync(Guid id)
      => await table.FindAsync(id);
}
