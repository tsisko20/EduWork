using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        [StringLength(200)]
        public string Description { get; set; } = null!;
        public bool IsFinished { get; set; }
        public bool IsEducation { get; set; }
        public bool IsPayable { get; set; }
        [Required]
        [StringLength(200)]
        public string DevopsProjectId { get; set; } = string.Empty;
        ICollection<ProjectTime> ProjectTime { get; set; } = null!;
        ICollection<UserProjectRole> UserProjectRoles { get; set; } = null!;
    }
}
