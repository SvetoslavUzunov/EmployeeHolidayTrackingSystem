using EmployeeHolidayTrackingSystem.Core.Constants;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Models.HolidayRequest;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHolidayTrackingSystem.Api.Controllers;

[Route(WebConstants.ControllerRoute)]
[ApiController]
public class HolidayRequestController : ControllerBase
{
   private readonly IHolidayRequestService holidayRequestService;

   public HolidayRequestController(IHolidayRequestService holidayRequestService)
      => this.holidayRequestService = holidayRequestService;

   [HttpGet(WebConstants.ActionRouteId)]
   public async Task<HolidayRequestModel> GetById(Guid id)
      => await holidayRequestService.GetByIdAsync(id);

   [HttpGet(WebConstants.ActionRoute)]
   public async Task<IEnumerable<HolidayRequestModel>> GetAll()
      => await holidayRequestService.GetAllAsync();

   [HttpPost(WebConstants.ActionRoute)]
   public async Task<HolidayRequestModel> Create([FromBody] HolidayRequestModel holidayRequest)
      => await holidayRequestService.CreateAsync(holidayRequest);

   [HttpPut(WebConstants.ActionRoute)]
   public async Task<HolidayRequestModel> Edit([FromBody] HolidayRequestModel holidayRequest)
      => await holidayRequestService.EditAsync(holidayRequest);

   [HttpDelete(WebConstants.ActionRouteId)]
   public async Task DeleteById(Guid id)
      => await holidayRequestService.DeleteByIdAsync(id);
}
