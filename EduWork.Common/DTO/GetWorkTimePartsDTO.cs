using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public record GetWorkTimePartsDTO
    {
        public int UserId { get; set; }
        public int? Day { get; set; } 
        public int? Month { get; set; } 
        public int? Year { get; set; }
    }
}
