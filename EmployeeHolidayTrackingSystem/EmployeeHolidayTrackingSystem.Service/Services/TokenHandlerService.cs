using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Exceptions;
using EmployeeHolidayTrackingSystem.Core.Models.Token;
using EmployeeHolidayTrackingSystem.Core.Provider;
using EmployeeHolidayTrackingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeHolidayTrackingSystem.Service.Services;

public class TokenHandlerService : ITokenHandlerService
{
   private readonly UserManager<UserEntity> userManager;
   private readonly IConfiguration configuration;
   private readonly IMemoryCache memoryCache;

   public TokenHandlerService(UserManager<UserEntity> userManager,
      IConfiguration configuration,
      IMemoryCache memoryCache)
   {
      this.userManager = userManager;
      this.configuration = configuration;
      this.memoryCache = memoryCache;
   }

   public async Task<TokenModel> GenerateToken(UserEntity user)
   {
      var tokenModel = new TokenModel()
      {
         AccessToken = await GenerateAccessToken(user),
         RefreshToken = GenerateRefreshToken()
      };

      memoryCache.Set(tokenModel.RefreshToken, user.Id, DateTimeOffset.Now.AddMinutes(CacheConstants.ExpirationTime));

      return tokenModel;
   }

   public string ValidateRefreshToken(string refreshToken)
   {
      var userId = memoryCache.Get(refreshToken).ToString();

      if (userId == null)
      {
         throw new ValidationException();
      }
      else
      {
         memoryCache.Remove(refreshToken);
      }

      return userId;
   }

   public async Task<IList<string>> GetUserRoles(UserEntity user)
      => await userManager.GetRolesAsync(user);

   private async Task<string> GenerateAccessToken(UserEntity user)
   {
      var claims = new List<Claim>()
      {
         new Claim(ClaimTypes.Name, user.UserName)
      };

      var userRoles = await GetUserRoles(user);

      foreach (var userRole in userRoles)
      {
         claims.Add(new Claim(ClaimTypes.Role, userRole));
      }

      var tokenKey = new TokenSettings(configuration).KeyAsEncoding;
      var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

      var tokenOptions = new JwtSecurityToken
      (
         claims: claims,
         expires: DateTime.Now.AddDays(TokenConstants.CountDays),
         signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
   }

   private static string GenerateRefreshToken()
   {
      var randomNumber = new byte[32];

      using var randomNumberGenerator = RandomNumberGenerator.Create();
      randomNumberGenerator.GetBytes(randomNumber);

      return Convert.ToBase64String(randomNumber);
   }
}
