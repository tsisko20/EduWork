using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class WorkDay
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public User User { get; set; } = new User();
        [Required]
        public DateOnly WorkDate { get; set; }
        public ICollection<ProjectTime>? ProjectTimes { get; set; }
        public ICollection<WorkDayTime>? WorkDayTimes { get; set; }
    }
}