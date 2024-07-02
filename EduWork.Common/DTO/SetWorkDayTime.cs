
namespace EduWork.Common.DTO
{
    public class SetWorkDayTime
    {
        public int UserId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Day { get; set; }
    }
}
