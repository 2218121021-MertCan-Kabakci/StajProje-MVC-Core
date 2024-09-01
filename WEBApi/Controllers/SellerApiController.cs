using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        [HttpGet("GetSellerInfo")]
        public List<Seller> GetSellerInfo()
        {
            List<Seller> sellers = _context.Sellers.ToList();

            return sellers;
        }
        [HttpPost("AddSeller")]
        public  bool AddSeller(Seller seller)
        {
            _context.Sellers.Add(seller);
            _context.SaveChanges();
            return true;
        }
        [HttpPost("UpdateSeller")]
        public IActionResult UpdateSeller(Seller seller)
        {
            if (seller == null && seller.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedSeller = _context.Sellers.Find(seller.Id);
            if (UpdatedSeller == null)
            {
                return BadRequest();
            }
            UpdatedSeller.Id = seller.Id;
            UpdatedSeller.ProductId = seller.ProductId;
            UpdatedSeller.UserId = seller.UserId;
            UpdatedSeller.Status = seller.Status;
            _context.Sellers.Update(UpdatedSeller);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("DeleteSeller")]
        public IActionResult DeleteSeller(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var seller = _context.Sellers.Find(id);
            if (seller == null)
            {
                return NotFound();
            }
            seller.Status = 2;
            _context.Sellers.Update(seller);
            _context.SaveChanges();
            return Ok(seller);
        }
    }
}
