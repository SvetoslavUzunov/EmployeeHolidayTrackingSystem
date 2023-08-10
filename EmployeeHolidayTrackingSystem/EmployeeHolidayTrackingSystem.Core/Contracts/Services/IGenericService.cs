namespace EmployeeHolidayTrackingSystem.Core.Contracts.Services;

public interface IGenericService<TModel> where TModel : class
{
   public Task<TModel> GetByIdAsync(Guid id);

   public Task<IEnumerable<TModel>> GetAllAsync();

   public Task<TModel> CreateAsync(TModel model);

   public Task<TModel> EditAsync(TModel model);

   public Task DeleteByIdAsync(Guid id);
}
