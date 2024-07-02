using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class WorkDay
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; } = null!;
        [Required]
        public DateOnly WorkDate { get; set; }
        public virtual ICollection<ProjectTime>? ProjectTimes { get; set; }
        public virtual ICollection<WorkDayTime>? WorkDayTimes { get; set; }
    }
}