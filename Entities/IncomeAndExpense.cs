using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class IncomeAndExpense
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int Explanation { get; set; }
        public DateTime CreateDate { get; set; }
        public int Description { get; set; }
        public string CurrencyUnit { get; set; }
        public int Status { get; set; }
        
    }
}
