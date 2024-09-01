using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeAndExpenseApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        [HttpGet("GetIAEInfo")]
        public List<IncomeAndExpense> GetIAEInfo()
        {
            List<IncomeAndExpense> ıncomeAndExpenses = _context.IncomeAndExpenses.ToList(); 

            return ıncomeAndExpenses;
        }
        [HttpPost("AddIAE")]
        public bool AddIAE(IncomeAndExpense ıncomeAndExpense)
        {
            _context.IncomeAndExpenses.Add(ıncomeAndExpense);
            _context.SaveChanges();
            return true;

        }
        [HttpPost("UpdateIAE")]
        public IActionResult UpdateIAE(IncomeAndExpense ıncomeAndExpense)
        {
            if (ıncomeAndExpense == null && ıncomeAndExpense.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedIAE = _context.IncomeAndExpenses.Find(ıncomeAndExpense.Id);
            if (UpdatedIAE == null)
            {
                return BadRequest();
            }
            UpdatedIAE.Id = ıncomeAndExpense.Id;
            //UpdatedIAE.SellerId = ıncomeAndExpense.SellerId;
            //UpdatedIAE.SalesId = ıncomeAndExpense.SalesId;
            UpdatedIAE.CreateDate = DateTime.Now;
            UpdatedIAE.Status = ıncomeAndExpense.Status;
            _context.IncomeAndExpenses.Update(UpdatedIAE);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("DeleteIAE")]
        public IActionResult DeleteIAE(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var IAE = _context.IncomeAndExpenses.Find(id);
            if (IAE == null)
            {
                return NotFound();
            }
            IAE.Status = 2;
            _context.IncomeAndExpenses.Update(IAE);
            _context.SaveChanges();
            return Ok(IAE);
        }
    }
}
