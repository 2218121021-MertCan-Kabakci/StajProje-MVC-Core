using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Billing
    {
            public int Id { get; set; }
            public string No { get; set; }
            public DateTime BillingDate { get; set; }
            public int Account { get; set; }
            public int Status { get; set; }
        
    }
}
