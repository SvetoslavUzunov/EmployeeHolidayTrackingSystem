namespace EmployeeHolidayTrackingSystem.Core.Exceptions;

public class ValidationException : Exception
{
   public ValidationException(string message = "Wrong refresh token!") : base(message) { }

   public ValidationException(IEnumerable<string> errors) : base(errors.ToString()) { }
}
