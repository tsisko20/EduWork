
using EduWork.Common.ValidationRules;
using System.ComponentModel.DataAnnotations;

namespace EduWork.Common.DTO
{
    public record SetWorkDayTimeDTO
    {
        public int UserId { get; set; }
        [Required]
        [ValidMinutes]
        public TimeOnly StartTime { get; set; }
        [Required]
        [ValidMinutes]
        public TimeOnly EndTime { get; set; }
        [Required]
        public DateOnly WorkDate { get; set; }
    }
}
