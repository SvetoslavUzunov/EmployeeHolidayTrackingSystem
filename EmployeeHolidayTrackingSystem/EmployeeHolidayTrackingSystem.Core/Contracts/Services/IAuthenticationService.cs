using EmployeeHolidayTrackingSystem.Core.Models.Token;
using EmployeeHolidayTrackingSystem.Core.Models.User;

namespace EmployeeHolidayTrackingSystem.Core.Contracts.Services;

public interface IAuthenticationService
{
   public Task<UserLoginModel> RegisterAsync(UserRegistrationModel userModel);

   public Task<TokenModel> LoginAsync(UserLoginModel userModel);

   public string Logout(string refreshToken);

   public Task<TokenModel> RefreshTokenAsync(string refreshToken);
}
