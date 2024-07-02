using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Username { get; set; } = string.Empty;
        [StringLength(50)]
        [Required]
        public string Email { get; set; } = string.Empty;
        public int? AppRoleId { get; set; }
        public virtual AppRole? AppRole { get; set; }
        [StringLength(200)]
        [Required]
        public string EntraObjectId { get; set; } = string.Empty;
        public virtual ICollection<AnnualLeaveRecord>? AnnualLeaveRecords { get; set; }
        public virtual ICollection<SickLeaveRecord>? SickLeaveRecords { get; set; }
        [Required]
        public virtual ICollection<AnnualLeave> AnnualLeaves { get; set; } = null!;
        public virtual ICollection<UserProjectRole>? UserProjectRoles { get; set; }
        public virtual ICollection<WorkDay>? WorkDays { get; set; }
    }
}
