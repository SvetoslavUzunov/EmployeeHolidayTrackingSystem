namespace EmployeeHolidayTrackingSystem.Core.Exceptions;

public class EmptyCollectionException : Exception
{
   public EmptyCollectionException(string message = "The collection is empty!") : base(message) { }

}
