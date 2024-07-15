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

        // POST
        [HttpPost]
        public async Task<ActionResult> PostWorkDayTime(SetWorkDayTimeDTO workDayTime)
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
        public async Task<IActionResult> PutWorkDayTimePartsAsync(List<UpdateWorkTimePartsDTO> workTimeParts)
        {         
            await _workTimeService.PutWorkTimePartsAsync(workTimeParts);
            return Ok();
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> DeleteWorkDayTimeParts(List<int> workTimeParts)
        {
            await _workTimeService.DeleteWorkTimePartsAsync(workTimeParts);
            return Ok();
        }

    }
}
