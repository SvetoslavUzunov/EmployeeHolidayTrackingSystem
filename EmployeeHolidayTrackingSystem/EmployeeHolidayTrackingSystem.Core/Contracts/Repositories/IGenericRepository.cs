namespace EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<TEntity> GetByIdAsync(Guid id);

    public Task<IEnumerable<TEntity>> GetAllAsync();

    public Task CreateAsync(TEntity entity);

    public void Edit(TEntity entity);

    public Task DeleteByIdAsync(Guid id);
}
