using ElsaYazilim.Helpers;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class StockController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public StockController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            List<StockDto> dto = JsonConvert.DeserializeObject<List<StockDto>>(await _service.GetMethodWithReturn($"StockApi/GetStockInfo"));

            ViewBag.Products = JsonConvert.DeserializeObject<List<Product>>(await _service.GetMethodWithReturn($"StockApi/GetProductsForSelect"));

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> GetStockInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/StockApi/GetStockInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Stock>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] StockDto stock)
        {
            
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(stock, $"StockApi/AddStock", null));
            return Json(new { success = isAdded });
            
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStock([FromBody] StockDto stock)
        {
            var UpdatedStock = JsonConvert.DeserializeObject<StockDto>(await _service.PostMethodWithReturn(stock, $"StockApi/UpdateStock", null));
            return Ok(UpdatedStock);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteStock(int Id)
        {
            var DeleteStock = JsonConvert.DeserializeObject<StockDto>(await _service.GetMethodWithReturn($"StockApi/DeleteStock?Id={Id}"));
            return Json(new { success = DeleteStock });
        }
    }
}
