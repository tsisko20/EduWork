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
        
        // GET: api/WorkDayTimes/5
        [HttpGet]
        public async Task<ActionResult<List<WorkTimePart>>> GetWorkDayTimeForUser(int userId, int d, int m, int y)
        {
            return await _workTimeService.GetWorkTimePartsForUserAsync(userId, d, m, y);
        }

        // PUT: api/WorkDayTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{workTimePart}")]
        public async Task<IActionResult> PutWorkDayTime(WorkTimePart workTimePart)
        {         
            await _workTimeService.PutWorkTimeRecordAsync(workTimePart);
            return Ok();
        }

        // POST: api/WorkDayTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostWorkDayTime(SetWorkDayTime workDayTime)
        {
            await _workTimeService.SetWorkTimeRecordAsync(workDayTime.UserId, workDayTime.StartTime, workDayTime.EndTime, workDayTime.Day);

            return Ok();
        }

        // DELETE: api/WorkDayTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDayTime(int id)
        {
            await _workTimeService.DeleteWorkTimeRecordAsync(id);
            return Ok();
        }

    }
}
