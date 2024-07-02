using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class ProjectTime
    {
        public int Id { get; set; }
        [Required]
        public int WorkDayId { get; set; }
        public virtual WorkDay WorkDay { get; set; } = new WorkDay();
        public int ProjectId { get; set; }
        [Required]
        public virtual Project Project { get; set; } = new Project();
        [StringLength(200)]
        public string? Comment { get; set; }
        [Required]
        public int TimeSpentMinutes { get; set; }
    }
}