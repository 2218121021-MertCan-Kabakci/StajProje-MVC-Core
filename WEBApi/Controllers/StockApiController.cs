using DataAccess;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        [HttpGet("GetStockInfo")]
        public List<StockDto> GetStockInfo()
        {
            var StockList = (from st in _context.Stocks.Where(x => x.Status == 1)
                             join pr in _context.Products.Where(x => x.Status == 1) on st.ProductId equals pr.Id
                             select new StockDto
                             {
                                 Id = st.Id,
                                 StockId = st.Id,
                                 ProductId = pr.Id,
                                 ProductName = pr.ProductName,
                                 Amount = st.Amount,
                                 EndTime = st.EndTime.ToString("yyyy-MM-dd HH:mm"),
                                 EntryTime = st.EntryTime.ToString("yyyy-MM-dd HH:mm"),
                                 SellPrice = pr.SellPrice,
                             }).ToList();

            return StockList;
        }

        [HttpGet("GetProductsForSelect")]
        public List<Product> GetProductsForSelect()
        {
            var productList = _context.Products.Where(x => x.Status == 1).ToList();

            return productList;
        }

        [HttpPost("AddStock")]
        public bool AddStock(StockDto stockDto)
        {
            
            var stock = new Stock()
            {
                Id=stockDto.Id,
                Amount = stockDto.Amount,
                ProductId = stockDto.ProductId,
                EndTime = Convert.ToDateTime(stockDto.EndTime),
                EntryTime = Convert.ToDateTime(stockDto.EntryTime),
                Status = 1

            };
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return true;

        }
        [HttpPost("UpdateStock")]
        public IActionResult UpdateStock(StockDto stockDto)
        {
            if (stockDto == null && stockDto.ProductId == 0)
            {
                return BadRequest();
            }
            //var UpdatedStock = _context.Stocks.Find(stockDto.StockId);
            var UpdatedStock = _context.Stocks.Where(x=>x.Id == stockDto.StockId).FirstOrDefault();
            var UpdatedProduct = _context.Products.Where(x=> x.Id == stockDto.ProductId).FirstOrDefault();
            
            if (UpdatedStock == null)
            {
                return BadRequest();
            }
            UpdatedProduct.SellPrice = stockDto.SellPrice;
            UpdatedStock.ProductId = stockDto.ProductId;
            UpdatedStock.Amount = stockDto.Amount;
            UpdatedStock.EndTime = Convert.ToDateTime(stockDto.EndTime);
            UpdatedStock.EntryTime = Convert.ToDateTime(stockDto.EntryTime);
            
            _context.Stocks.Update(UpdatedStock);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("DeleteStock")]
        public IActionResult DeleteStock(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Status = 2;
            _context.Stocks.Update(stock);
            _context.SaveChanges();
            return Ok(stock);
        }
    }
}
