using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeHolidayTrackingSystem.Core.Provider;

public class TokenSettings
{
   private readonly IConfiguration configuration;

   public TokenSettings(IConfiguration configuration)
      => this.configuration = configuration;

   public SymmetricSecurityKey KeyAsEncoding => GetKeyAsEncoding();

   public string KeyAsString => GetKeyAsString();

   public string Audience => GetAudience();

   public string Issue => GetIssue();

   private SymmetricSecurityKey GetKeyAsEncoding()
      => new(Encoding.UTF8.GetBytes(configuration.GetSection("TokenOptions:Key").Value));

   private string GetKeyAsString()
      => configuration.GetSection("TokenOptions:Key").Value;

   private string GetAudience()
      => configuration.GetSection("TokenOptions:Audience").Value;

   private string GetIssue()
      => configuration.GetSection("TokenOptions:Issue").Value;
}
