using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class UserProjectRole
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int ProjectId { get; set; }
        [Required]
        public Project Project { get; set; } = new Project();
        public int? ProjectRoleId { get; set; }
        public ProjectRole? ProjectRole { get; set; }
    }
}