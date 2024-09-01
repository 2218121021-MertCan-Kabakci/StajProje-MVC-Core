using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        #region Ürün Bilgileri Listesi
        [HttpGet("GetProductInfo")]
        public List<Product> GetProductInfo()
        {
            List<Product> products = _context.Products.ToList();

            return products;
        }
        #endregion

        #region Ürün Ekleme İşlemleri
        [HttpPost("AddProduct")]
        public bool AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return true;

        }
        #endregion

        #region Ürün Güncelleme İşlemleri

        [HttpPost("UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            if (product == null && product.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedProduct = _context.Products.Find(product.Id);
            if (UpdatedProduct == null)
            {
                return BadRequest();
            }
            UpdatedProduct.ProductName = product.ProductName;
            UpdatedProduct.PurchasePrice = product.PurchasePrice;
            UpdatedProduct.SellPrice = product.SellPrice;
            UpdatedProduct.Amount = product.Amount;
            UpdatedProduct.CreatedDate = DateTime.Now;
            UpdatedProduct.SellerId = product.SellerId;
            UpdatedProduct.CategoryId = product.CategoryId;
            UpdatedProduct.Id = product.Id;
            UpdatedProduct.Status = product.Status;
            _context.Products.Update(UpdatedProduct);
            _context.SaveChanges();
            return Ok();
        }
        #endregion

        #region Ürün Silme İşlemleri
        [HttpGet("DeleteProduct")]
        public IActionResult DeleteProduct(int id) 
        {
        if(id < 1)
            {
                return BadRequest();
            }
            var product = _context.Products.Find(id);
            if (product == null) 
            {
                return NotFound();            
            }
            product.Status = 2;
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }
        #endregion

    }
}
