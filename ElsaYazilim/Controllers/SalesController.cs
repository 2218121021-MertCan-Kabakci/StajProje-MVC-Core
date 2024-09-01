using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class SalesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public SalesController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }
        AppDbContext _context = new AppDbContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/SalesApi/GetSalesInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Sales>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddSales(Sales sales)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(sales, $"SalesApi/AddSales", null));
            return Json(new { succses = isAdded });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSales(Sales sales)
        {
            var UpdatedSales = JsonConvert.DeserializeObject<Sales>(await _service.PostMethodWithReturn(sales, $"SalesApi/UpdateSales", null));
            return View(UpdatedSales);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSales(int Id)
        {
            var DeleteSales = JsonConvert.DeserializeObject<Sales>(await _service.GetMethodWithReturn($"SalesApi/DeleteSales?Id={Id}"));
            return View(DeleteSales);
        }
    }
}
