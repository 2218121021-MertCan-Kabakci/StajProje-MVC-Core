using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime EntryTime { get; set; }
        
        public DateTime EndTime { get; set; }
    }
}
