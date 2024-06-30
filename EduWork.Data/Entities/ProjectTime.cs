using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class ProjectTime
    {
        public int Id { get; set; }
        [Required]
        public WorkDay WorkDay { get; set; } = new WorkDay();
        [Required]
        public Project Project { get; set; } = new Project();
        [StringLength(200)]
        public string? Comment { get; set; }
        [Required]
        public int TimeSpentMinutes { get; set; }
    }
}