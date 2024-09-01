using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        [HttpGet("GetSalesInfo")]
        public List<Sales> GetSalesInfo()
        {
            List<Sales> sales = _context.Sales.ToList();

            return sales;
        }
        [HttpPost("AddSales")]
        public  bool AddSales(Sales sales)
        {
            _context.Sales.Add(sales);
            _context.SaveChanges();
            return true;

        }
        [HttpPost("UpdateSales")]
        public IActionResult UpdateSales(Sales sales)
        {
            if (sales == null && sales.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedSales = _context.Sales.Find(sales.Id);
            if (UpdatedSales == null)
            {
                return BadRequest();
            }
            UpdatedSales.Id = sales.Id;
            UpdatedSales.SaleDate = DateTime.Now;
            UpdatedSales.ProductId = sales.ProductId;
            UpdatedSales.CategoryId = sales.CategoryId;
            UpdatedSales.CustomerId = sales.CustomerId;
            UpdatedSales.SaleAmount = sales.SaleAmount;
            UpdatedSales.Status = sales.Status;
            _context.Sales.Add(UpdatedSales);
            _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("DeleteSales")]
        public IActionResult DeleteSales(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var sales = _context.Sales.Find(id);
            if (sales == null)
            {
                return NotFound();
            }
            sales.Status = 2;
            _context.Sales.Update(sales);
            _context.SaveChanges();
            return Ok(sales);
        }
    }
}
