using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data.Entities
{
    public class AnnualLeave
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; } = new User();
        public int Year { get; set; }
        public int TotalLeaveDays { get; set; }
        public int LeftLeaveDays { get; set; }  
    }
}
