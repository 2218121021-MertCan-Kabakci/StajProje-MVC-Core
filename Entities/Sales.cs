using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SaleAmount { get; set; }
        public int  CustomerId{ get; set; }
        public int CategoryId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Status { get; set; }
    }
}
