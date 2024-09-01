using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public string EntryTime { get; set; }
        public string EndTime { get; set; }
        public string ProductName { get; set; }
        public float BuyPrice { get; set; }
        public float SellPrice { get; set; }
        public int Amount { get; set; }
    }
}
