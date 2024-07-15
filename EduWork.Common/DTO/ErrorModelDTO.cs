using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.DTO
{
    public class ErrorModelDTO
    {
        public string? Title { get; set; }
        public int Status { get; set; }
        public List<string>? Errors { get; set; }
    }
}
