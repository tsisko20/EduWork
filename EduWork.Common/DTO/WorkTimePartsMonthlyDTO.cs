using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public record WorkTimePartsMonthlyDTO
    {
        public int UserId { get; set; }
        public int Year { get; set; }
    }
}
