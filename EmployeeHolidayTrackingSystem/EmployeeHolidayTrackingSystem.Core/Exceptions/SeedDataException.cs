namespace EmployeeHolidayTrackingSystem.Core.Exceptions;

public class SeedDataException : Exception
{
   public SeedDataException(string message = "User already is in this role!") : base(message) { }
}
