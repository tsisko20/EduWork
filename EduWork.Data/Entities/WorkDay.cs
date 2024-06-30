using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class WorkDay
    {
        public int Id { get; set; }
        [Required]
        public ICollection<User> Users { get; set; } = null!;
        [Required]
        public DateOnly WorkDate { get; set; }
        public ProjectTime ProjectTime { get; set; } = null!;
        public WorkDayTime WorkDayTime { get; set; } = null!;
    }
}