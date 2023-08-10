using EmployeeHolidayTrackingSystem.Core.Models.Token;
using EmployeeHolidayTrackingSystem.Data.Entities;

namespace EmployeeHolidayTrackingSystem.Core.Contracts.Services;

public interface ITokenHandlerService
{
   public Task<TokenModel> GenerateToken(UserEntity user);

   public Task<IList<string>> GetUserRoles(UserEntity user);

   public string ValidateRefreshToken(string refreshToken);
}
