using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public CategoryController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }
        AppDbContext _context = new AppDbContext();
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/CategoryApi/GetCategoryInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/CategoryApi/GetCategoryInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category x)
        {
            try
            {
                bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(x, $"CategoryApi/AddCategory", null));
                return Json(new { success = isAdded });
            }
            catch (Exception ex)
            {
                // Hata mesajını görmek için loglama ekleyin veya hata mesajını geri döndürün
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var UpdatedCategory = JsonConvert.DeserializeObject<Category>(await _service.PostMethodWithReturn(category, $"CategoryApi/UpdateCategory", null));
            return View(UpdatedCategory);
      
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var DeleteCategory = JsonConvert.DeserializeObject<Category>(await _service.GetMethodWithReturn($"CategoryApi/DeleteCategory?Id={Id}"));
            return Json(new { success = DeleteCategory });
        }
    }
}
