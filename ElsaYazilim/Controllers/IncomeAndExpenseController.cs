using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class IncomeAndExpenseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;
        public IncomeAndExpenseController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }
        AppDbContext _context = new AppDbContext();
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7151/api/IncomeAndExpenseApi/GetIAEInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<IncomeAndExpense>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetIAEUnfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/IncomeAndExpenseApi/GetIAEInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<IncomeAndExpense>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddIAE(IncomeAndExpense ıncomeAndExpense)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(ıncomeAndExpense, $"IncomeAndExpenseApi/AddIAE", null));
            return Json(new { succses = isAdded });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIAE(IncomeAndExpense ıncomeAndExpense)
        {
            var UpdatedProduct = JsonConvert.DeserializeObject<Product>(await _service.PostMethodWithReturn(ıncomeAndExpense, $"IncomeAndExpenseApi/UpdateIAE", null));
            return View(UpdatedProduct);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteIAE(int Id)
        {
            var DeleteProduct = JsonConvert.DeserializeObject<Product>(await _service.GetMethodWithReturn($"IncomeAndExpenseApi/DeleteIAE?Id={Id}"));
            return View(DeleteProduct);
        }

    }
}
