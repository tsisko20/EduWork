
using System.ComponentModel.DataAnnotations;

namespace EduWork.Common.DTO
{
    public class SetWorkDayTimeDTO
    {
        public int UserId { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public DateOnly WorkDate { get; set; }
    }
}
