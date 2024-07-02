using System.ComponentModel.DataAnnotations;

namespace EduWork.Data.Entities
{
    public class UserProjectRole
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public int ProjectId { get; set; }
        [Required]
        public virtual Project Project { get; set; } = null!;
        public int? ProjectRoleId { get; set; }
        public virtual ProjectRole? ProjectRole { get; set; }
    }
}