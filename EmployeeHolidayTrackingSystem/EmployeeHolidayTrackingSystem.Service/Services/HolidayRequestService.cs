using EmployeeHolidayTrackingSystem.Core.Contracts.Patterns;
using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Enums;
using EmployeeHolidayTrackingSystem.Core.Exceptions;
using EmployeeHolidayTrackingSystem.Core.Models.HolidayRequest;
using EmployeeHolidayTrackingSystem.Data.Entities;

namespace EmployeeHolidayTrackingSystem.Service.Services;

public class HolidayRequestService : IHolidayRequestService
{
   private readonly IHolidayRequestRepository holidayRequestRepository;
   private readonly IUnitOfWork unitOfWork;

   public HolidayRequestService(IHolidayRequestRepository holidayRequestRepository, IUnitOfWork unitOfWork)
   {
      this.holidayRequestRepository = holidayRequestRepository;
      this.unitOfWork = unitOfWork;
   }

   public async Task<HolidayRequestModel> GetByIdAsync(Guid id)
   {
      var holidayRequest = await holidayRequestRepository.GetByIdAsync(id);

      if (holidayRequest == null)
      {
         throw new ItemNotFoundException();
      }

      return new HolidayRequestModel
      {
         Id = holidayRequest.Id,
         StartDate = holidayRequest.StartDate,
         EndDate = holidayRequest.EndDate,
         User = holidayRequest.User,
         Status = holidayRequest.Status,
         HolidayRequestCountDays = holidayRequest.HolidayRequestCountDays,
         IsActive = holidayRequest.IsActive
      };
   }

   public async Task<IEnumerable<HolidayRequestModel>> GetAllAsync()
   {
      var holidayRequests = await holidayRequestRepository.GetAllAsync();

      if (!holidayRequests.Any())
      {
         throw new EmptyCollectionException();
      }

      return holidayRequests.Select(u => new HolidayRequestModel
      {
         Id = u.Id,
         StartDate = u.StartDate,
         EndDate = u.EndDate,
         SupervisorDisapprovedComment = u.SupervisorDisapprovedComment,
         UserId = u.UserId,
         Status = u.Status,
         HolidayRequestCountDays = u.HolidayRequestCountDays,
         CreatedOn = u.CreatedOn,
         IsActive = u.IsActive
      })
      .ToList();
   }

   public async Task<HolidayRequestModel> CreateAsync(HolidayRequestModel holidayRequestModel)
   {
      var holidayRequest = await holidayRequestRepository.GetByIdAsync(holidayRequestModel.Id);

      if (holidayRequest != null)
      {
         throw new ItemAlreadyExistException();
      }

      holidayRequest = new HolidayRequestEntity
      {
         StartDate = holidayRequestModel.StartDate.ToLocalTime(),
         EndDate = holidayRequestModel.EndDate.ToLocalTime(),
         SupervisorDisapprovedComment = holidayRequestModel.SupervisorDisapprovedComment,
         UserId = holidayRequestModel.UserId,
         Status = HolidayRequestStatusEnum.Pending,
         HolidayRequestCountDays = holidayRequestModel.HolidayRequestCountDays,
         CreatedOn = DateTime.Now,
         IsActive = holidayRequestModel.IsActive
      };

      await holidayRequestRepository.CreateAsync(holidayRequest);
      await unitOfWork.CompleteAsync();

      return holidayRequestModel;
   }

   public async Task<HolidayRequestModel> EditAsync(HolidayRequestModel holidayRequestModel)
   {
      var holidayRequest = await holidayRequestRepository.GetByIdAsync(holidayRequestModel.Id);

      if (holidayRequest == null)
      {
         throw new ItemNotFoundException();
      }

      holidayRequest.Id = holidayRequestModel.Id;
      holidayRequest.StartDate = holidayRequestModel.StartDate.ToLocalTime();
      holidayRequest.EndDate = holidayRequestModel.EndDate.ToLocalTime();
      holidayRequest.SupervisorDisapprovedComment = holidayRequestModel.SupervisorDisapprovedComment;
      holidayRequest.Status = holidayRequestModel.Status;
      holidayRequest.UserId = holidayRequestModel.UserId;
      holidayRequest.IsActive = holidayRequestModel.IsActive;

      holidayRequestRepository.Edit(holidayRequest);
      await unitOfWork.CompleteAsync();

      return holidayRequestModel;
   }

   public async Task DeleteByIdAsync(Guid id)
   {
      var holidayRequest = await holidayRequestRepository.GetByIdAsync(id);

      if (holidayRequest == null || !holidayRequest.IsActive)
      {
         throw new ItemNotFoundException();
      }

      holidayRequest.IsActive = false;
      await unitOfWork.CompleteAsync();
   }
}
