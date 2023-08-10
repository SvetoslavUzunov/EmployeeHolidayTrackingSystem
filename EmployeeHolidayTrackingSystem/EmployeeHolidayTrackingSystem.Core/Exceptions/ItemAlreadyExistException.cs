namespace EmployeeHolidayTrackingSystem.Core.Exceptions;

public class ItemAlreadyExistException : Exception
{
   public ItemAlreadyExistException(string message = "Item already exist!") : base(message) { }
}
