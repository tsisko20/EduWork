using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class UserProjectRole
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; } = new User();
        [Required]
        public Project Project { get; set; } = new Project();
        [Required]
        public ProjectRole ProjectRole { get; set; } = new ProjectRole();
    }
}