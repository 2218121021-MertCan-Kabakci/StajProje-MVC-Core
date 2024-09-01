using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float SellPrice { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public float PurchasePrice { get; set; }
        public int Status { get; set; }
    }
}
