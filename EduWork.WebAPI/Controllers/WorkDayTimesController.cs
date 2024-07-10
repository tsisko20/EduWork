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
        public async Task<ActionResult<List<WorkTimePartDTO>>> GetWorkDayTimeForUser([FromQuery]RequestWorkTimePartsDTO getWorkTimeParts)
        {
            return await _workTimeService.GetWorkTimePartsForUserAsync(getWorkTimeParts);
        }

        // PUT
        [HttpPut("{workTimePart}")]
        public async Task<IActionResult> PutWorkDayTime(WorkTimePartDTO workTimePart)
        {         
            await _workTimeService.PutWorkTimeRecordAsync(workTimePart);
            return Ok();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> PostWorkDayTime(SetWorkDayTimeDTO workDayTime)
        {
            await _workTimeService.SetWorkTimeRecordAsync(workDayTime);
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
