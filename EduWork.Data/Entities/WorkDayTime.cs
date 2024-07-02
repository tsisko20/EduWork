using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class WorkDayTime
    {
        public int Id { get; set; }
        public int WorkDayId { get; set; }
        [Required]
        public virtual WorkDay WorkDay { get; set; } = new WorkDay();
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}