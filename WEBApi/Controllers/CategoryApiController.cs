using DataAccess;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        [HttpGet("GetCategoryInfo")]
        public List<Category> GetCategoryInfo()
        {
            List<Category> categories = _context.Categories.Where(x=>x.Status == 1).ToList();

            return categories;
        }
        [HttpPost("AddCategory")]
        public bool AddCategory(Category x)
        {
            var category = new Category()
            {   
                Id = x.Id,
                Name = x.Name,
                Status = 1,
                ParentCategoryId = x.ParentCategoryId,
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }
        [HttpPost("UpdateCategory")]
        public IActionResult UpdateCategory(Category category)
        {
            if (category == null && category.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedCategory = _context.Categories.Find(category.Id);
            if (UpdatedCategory == null)
            {
                return BadRequest();
            }
            UpdatedCategory.Id = category.Id;
            //UpdatedCategory.ParentCategoryId = category.ParentCategoryId;
            UpdatedCategory.Status = category.Status;
            UpdatedCategory.Name = category.Name;
            _context.Categories.Add(UpdatedCategory);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Status = 2;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Ok(category);
        }
    }
}
