using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class WorkDayTime
    {
        public int Id { get; set; }
        public ICollection<WorkDay> WorkDays { get; set; } = null!;
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}