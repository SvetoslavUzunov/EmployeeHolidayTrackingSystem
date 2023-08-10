using Microsoft.Extensions.Configuration;

namespace EmployeeHolidayTrackingSystem.Data;

public class DBConnectionString
{
   private readonly IConfiguration configuration;

   public DBConnectionString() { }

   public DBConnectionString(IConfiguration configuration)
      => this.configuration = configuration;

   public string GetDbConnectionString
      => GetConnectionString();

   private string GetConnectionString()
      => configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
}
