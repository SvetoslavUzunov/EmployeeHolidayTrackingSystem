namespace EmployeeHolidayTrackingSystem.Core.Constants;

public static class UserConstants
{
   public const int UserNameMinLength = 1;
   public const int UserNameMaxLength = 30;

   public const int FirstNameMinLength = 1;
   public const int FirstNameMaxLength = 30;

   public const int LastNameMinLength = 1;
   public const int LastNameMaxLength = 30;

   public const int PasswordMinLength = 7;

   public const short InitialPaidLeavesCount = 20;

   public const string AdminUserName = "AdminUser";
   public const string AdminUserEmail = "admin@user.com";
   public const string AdminUserPassword = "Pass123#";

   public const string SupervisorUserName = "SupervisorUser";
   public const string SupervisorUserEmail = "supervisor@user.com";
   public const string SupervisorUserPassword = "Pass123#Supervisor";

   public const string UserTokenKey = "userToken";

   public const string UserAlreadyExist = "User already exist!";
   public const string UserNotFound = "User not found!";
}
