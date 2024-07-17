using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public record MonthlyWorkHoursDTO
    {
        public int Month { get; set; }
        public double TotalHours { get; set; }
    }
}
