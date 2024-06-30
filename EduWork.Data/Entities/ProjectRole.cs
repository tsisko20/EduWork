using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data.Entities
{
    public class ProjectRole
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Description { get; set; }
        public ICollection<UserProjectRole>? UserProjectRoles { get; set; }
    }
}
