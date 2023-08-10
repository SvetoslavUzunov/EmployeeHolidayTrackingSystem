using EmployeeHolidayTrackingSystem.Core.Enums;
using EmployeeHolidayTrackingSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static EmployeeHolidayTrackingSystem.Core.Constants.SupervisorConstants;

namespace EmployeeHolidayTrackingSystem.Core.Models.HolidayRequest;

public class HolidayRequestModel
{
   public Guid Id { get; set; } = Guid.NewGuid();

   public DateTime StartDate { get; set; }

   public DateTime EndDate { get; set; }

   [StringLength(DisapprovedPaidLeaveCommentMaxLength)]
   public string? SupervisorDisapprovedComment { get; set; }

   public HolidayRequestStatusEnum Status { get; set; }

   public int? HolidayRequestCountDays { get; set; }

   public Guid UserId { get; set; }

   public UserEntity? User { get; set; }

   public DateTime CreatedOn { get; set; }

   public bool IsActive { get; set; } = true;
}
