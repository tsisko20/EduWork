using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public record UpdateWorkTimePartDTO
    {
        public int WorkTimePartId { get; set; } 
        public TimeOnly StartTime { get; set;}
        public TimeOnly EndTime { get; set;}
    }
}
