using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Models.Token;
using EmployeeHolidayTrackingSystem.Core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHolidayTrackingSystem.Api.Controllers;

[Route(WebConstants.ControllerRoute)]
[ApiController]
public class AuthenticationController : ControllerBase
{
   private readonly IAuthenticationService authenticationService;

   public AuthenticationController(IAuthenticationService authenticationService)
      => this.authenticationService = authenticationService;

   [HttpPost(WebConstants.ActionRoute)]
   public async Task<UserLoginModel> Register([FromBody] UserRegistrationModel registrationModel)
      => await authenticationService.RegisterAsync(registrationModel);

   [HttpPost(WebConstants.ActionRoute)]
   public async Task<TokenModel> Login([FromBody] UserLoginModel loginModel)
      => await authenticationService.LoginAsync(loginModel);

   [HttpPost(WebConstants.ActionRoute)]
   public string Logout([FromBody] string refreshToken)
      => authenticationService.Logout(refreshToken);

   [HttpPost(WebConstants.ActionRoute)]
   public async Task<TokenModel> RefreshToken([FromBody] string refreshToken)
      => await authenticationService.RefreshTokenAsync(refreshToken);
}
