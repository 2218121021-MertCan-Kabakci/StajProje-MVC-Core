using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        #region CUSTOMER İŞLEMLERİ
        [HttpGet("GetCustomer")]
        public List<Customer> GetCustomer()
        {
            List<Customer> customers = _context.Customers.Where(x => x.Status == 1).ToList();

            return customers;
        }
        [HttpPost("AddCustomer")]
        public bool AddCustomer(Customer x)
        {
            var customer = new Customer()
            {
                Name = x.Name,
                TaxNo = x.TaxNo,
                TaxAdministration = x.TaxAdministration,
                Status = 1,

                //EndTime = Convert.ToDateTime(categoryDto.EndTime),
                //EntryTime = Convert.ToDateTime(categoryDto.EntryTime)
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region CUSTOMER GÜNCELLEME İŞLEMLERİ

        [HttpPost("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer customer)
        {
            if (customer == null && customer.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedCustomer = _context.Customers.Find(customer.Id);
            if (UpdatedCustomer == null)
            {
                return NotFound();
            }
            UpdatedCustomer.Name = customer.Name;
            UpdatedCustomer.Status = customer.Status;
            UpdatedCustomer.Id = customer.Id;
            UpdatedCustomer.TaxAdministration = customer.TaxAdministration;
            UpdatedCustomer.TaxNo = customer.TaxNo;
            _context.Customers.Update(UpdatedCustomer);
            _context.SaveChanges();
            return Ok();


        }
        #endregion

        #region MÜŞTERİ SİLME İŞLEMLERİ

        [HttpGet("DeleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Status = 2;
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return Ok(customer);

        }
        #endregion
    }
}
