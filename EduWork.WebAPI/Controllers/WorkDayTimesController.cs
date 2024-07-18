using Microsoft.AspNetCore.Mvc;
using EduWork.Domain.Services;
using EduWork.Common.DTO;
using EduWork.Data.Entities;


namespace EduWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDayTimesController : ControllerBase
    {
        private readonly WorkTimeService _workTimeService;

        public WorkDayTimesController(WorkTimeService workTimeService)
        {
            _workTimeService = workTimeService;
        }

        // GET: api/WorkDayTimes?userId=1&d=21&m=8&y=2007
        [HttpGet]
        public async Task<ActionResult<List<WorkTimePartDTO>>> GetWorkDayTimeForUserAsync([FromQuery]RequestWorkTimePartsDTO getWorkTimeParts)
        {
            return await _workTimeService.GetWorkTimePartsForUserAsync(getWorkTimeParts);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> PostWorkDayTimeAsync(SetWorkDayTimeDTO workDayTime)
        {
            try
            {
                await _workTimeService.SetWorkTimeRecordAsync(workDayTime);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return a 400 Bad Request with the error message
            }
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> PutWorkDayTimePartsAsync(UpdateWorkTimePartDTO update)
        {
            try
            {
                await _workTimeService.UpdateWorkTimePartAsync(update);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return a 400 Bad Request with the error message
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDayTimePartsAsync(int id)
        {
            try
            {
                await _workTimeService.DeleteWorkTimePartAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return a 400 Bad Request with the error message
            }
        }

        // GET: api/WorkDayTimes?userId=1&d=21&m=8&y=2007
        [HttpGet("monthly")]
        public async Task<ActionResult<List<MonthlyWorkHoursDTO>>> GetWorkDayTimeMonthlyAsync([FromQuery] RequestWorkTimePartsDTO getWorkTimeParts)
        {
            return await _workTimeService.GetWorkTimePartsMonthlyAsync(getWorkTimeParts);
        }

        // GET: api/WorkDayTimes?userId=1&d=21&m=8&y=2007
        [HttpGet("weekly")]
        public async Task<ActionResult<List<WeeklyWorkHoursDTO>>> GetWorkDayTimeWeeklyAsync([FromQuery] RequestWorkTimePartsDTO getWorkTimeParts)
        {
            return await _workTimeService.GetWorkTimePartsWeeklyAsync(getWorkTimeParts);
        }

        [HttpGet("holidays")]
        public async Task<ActionResult<List<NonWorkingDay>>> GetHolidays()
        {
            return await _workTimeService.GetNonWorkingDays();
        }

    }
}
