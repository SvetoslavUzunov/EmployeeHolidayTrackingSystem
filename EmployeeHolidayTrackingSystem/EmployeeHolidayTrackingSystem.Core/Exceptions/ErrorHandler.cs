using EmployeeHolidayTrackingSystem.Core.Models.Errors;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeHolidayTrackingSystem.Core.Exceptions;

public static class ErrorHandler
{
   public static void ExecuteErrorHandler(IdentityResult identityResult)
   {
      var errorModel = new ErrorModel();

      errorModel.Errors.AddRange(identityResult.Errors.Select(x => x.Description));

      throw new ValidationException(errorModel.Errors);
   }
}
