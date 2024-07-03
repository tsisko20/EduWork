using Microsoft.AspNetCore.Mvc;
using EduWork.Domain.Services;
using EduWork.Common.DTO;


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
        public async Task<ActionResult<List<WorkTimePart>>> GetWorkDayTimeForUser(int userId, int d, int m, int y)
        {
            return await _workTimeService.GetWorkTimePartsForUserAsync(userId, d, m, y);
        }

        // PUT
        [HttpPut("{workTimePart}")]
        public async Task<IActionResult> PutWorkDayTime(WorkTimePart workTimePart)
        {         
            await _workTimeService.PutWorkTimeRecordAsync(workTimePart);
            return Ok();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> PostWorkDayTime(SetWorkDayTime workDayTime)
        {
            await _workTimeService.SetWorkTimeRecordAsync(workDayTime.UserId, workDayTime.StartTime, workDayTime.EndTime, workDayTime.Day);

            return Ok();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDayTime(int id)
        {
            await _workTimeService.DeleteWorkTimeRecordAsync(id);
            return Ok();
        }

    }
}
