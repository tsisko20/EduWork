using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public record WeeklyWorkHoursDTO
    {
        public int WeekNumber { get; set; }
        public double TotalHours { get; set; }
    }
}
