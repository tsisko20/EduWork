
namespace EduWork.Common.DTO
{
    public class SetWorkDayTimeDTO
    {
        public int UserId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly WorkDate { get; set; }
    }
}
