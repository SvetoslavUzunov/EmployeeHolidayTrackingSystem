using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Models.Token;
using EmployeeHolidayTrackingSystem.Core.Models.User;
using EmployeeHolidayTrackingSystem.Data.Entities;
using EmployeeHolidayTrackingSystem.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace EmployeeHolidayTrackingSystem.Test.Services;

[TestClass]
public class AuthenticationServiceTest
{
   private IAuthenticationService authenticationService;
   private readonly Mock<IAuthenticationService> authenticationServiceMock = new();
   private readonly Mock<UserManager<UserEntity>> userManagerMock = new(Mock.Of<IUserStore<UserEntity>>(), null, null, null, null, null, null, null, null);
   private readonly Mock<ITokenHandlerService> tokenHandlerServiceMock = new();
   private readonly Mock<IMemoryCache> memoryCache = new();
   private UserRegistrationModel userRegistrationModel;
   private UserLoginModel userLoginModel;

   [TestInitialize]
   public void Setup()
   {
      authenticationService = new AuthenticationService(userManagerMock.Object, tokenHandlerServiceMock.Object, memoryCache.Object);
      userRegistrationModel = new UserRegistrationModel();
      userLoginModel = new UserLoginModel();
   }

   [TestMethod]
   public async Task RegisterMethodShoutRegisterUser()
   {
      userRegistrationModel = CreateRegistrationModel();

      authenticationServiceMock
         .Setup(x => x.RegisterAsync(It.IsAny<UserRegistrationModel>()));

      userManagerMock
         .Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
         .ReturnsAsync(IdentityResult.Success);

      userManagerMock
         .Setup(x => x.AddToRoleAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
         .ReturnsAsync(IdentityResult.Success);

      await authenticationService.RegisterAsync(userRegistrationModel);
   }

   [TestMethod]
   public async Task LoginMethodShoutSendAccessAndRefreshToken()
      => await LoginBase();

   [TestMethod]
   public async Task RefreshTokenMethodShoutReturnNewAccessToken()
   {
      authenticationServiceMock
         .Setup(x => x.RefreshTokenAsync(It.IsAny<string>()))
         .ReturnsAsync(It.IsAny<TokenModel>());

      tokenHandlerServiceMock
         .Setup(x => x.ValidateRefreshToken(It.IsAny<string>()))
         .Returns(It.IsAny<string>());

      var userEntity = CreateUserEntity();

      userManagerMock
         .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
         .ReturnsAsync(userEntity);

      var tokenModel = await LoginBase();

      await authenticationService.RefreshTokenAsync(tokenModel.RefreshToken);
   }

   private async Task<TokenModel> LoginBase()
   {
      userLoginModel = CreateLoginModel();

      authenticationServiceMock
         .Setup(x => x.LoginAsync(It.IsAny<UserLoginModel>()))
         .ReturnsAsync(It.IsAny<TokenModel>());

      var userEntity = CreateUserEntity();

      userManagerMock
         .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
         .ReturnsAsync(userEntity);

      userManagerMock
         .Setup(x => x.CheckPasswordAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
         .ReturnsAsync(true);

      tokenHandlerServiceMock
         .Setup(x => x.GenerateToken(It.IsAny<UserEntity>()))
         .ReturnsAsync(new TokenModel { AccessToken = String.Empty, RefreshToken = String.Empty });

      userManagerMock
         .Setup(x => x.GetRolesAsync(It.IsAny<UserEntity>()))
         .ReturnsAsync(new List<string>() { "Admin" });

      return await authenticationService.LoginAsync(userLoginModel);
   }

   private UserEntity CreateUserEntity()
   {
      return new UserEntity
      {
         UserName = userLoginModel.UserName,
         Email = userLoginModel.Email,
         PasswordHash = userLoginModel.Password
      };
   }

   private UserRegistrationModel CreateRegistrationModel()
   {
      userRegistrationModel.UserName = GetUserName();
      userRegistrationModel.Email = GetEmail();
      userRegistrationModel.Password = GetPassword();

      return userRegistrationModel;
   }

   private UserLoginModel CreateLoginModel()
   {
      userLoginModel.UserName = UserConstants.AdminUserName;
      userLoginModel.Email = UserConstants.AdminUserEmail;
      userLoginModel.Password = UserConstants.AdminUserPassword;

      return userLoginModel;
   }

   private static string GetUserName()
      => $"UserName:{new Random().Next(10)}";

   private static string GetEmail()
      => $"user{new Random().Next(10)}@abv.bg";

   private static string GetPassword()
      => $"TestPassword123#{new Random().Next(10)}";
}
