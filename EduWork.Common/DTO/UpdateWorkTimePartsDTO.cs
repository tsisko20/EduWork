using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public record UpdateWorkTimePartsDTO : WorkTimePartDTO
    {
        public bool Delete { get; set; } = false;
    }
}
