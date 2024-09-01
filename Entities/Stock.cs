using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Stock 
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        
    }
}
