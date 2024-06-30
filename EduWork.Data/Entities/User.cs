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
        public int AppRoleId { get; set; }
        public AppRole? AppRole { get; set; }
        [StringLength(200)]
        [Required]
        public string EntraObjectId { get; set; } = string.Empty;
        public ICollection<AnnualLeaveRecord>? AnnualLeaveRecords { get; set; }
        public ICollection<SickLeaveRecord>? SickLeaveRecords { get; set; }
        [Required]
        public ICollection<AnnualLeave> AnnualLeaves { get; set; } = null!;
        public ICollection<UserProjectRole>? UserProjectRoles { get; set; }
        public ICollection<WorkDay>? WorkDays { get; set; }
    }
}
