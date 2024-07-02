using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data.Entities
{
    public class AnnualLeaveRecord
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        [StringLength(200)]
        public string? Comment { get; set; }
    }
}
