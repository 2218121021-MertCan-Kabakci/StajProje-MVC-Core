using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class SellerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public SellerController(IHttpClientFactory httpClientFactory, Service service)
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
        public async Task<IActionResult> GetSellerInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/SellerApi/GetSellerInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Seller>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddSeller(Seller seller)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(seller, $"SellerApi/AddSeller", null));
            return Json(new { succses = isAdded });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSeller(Seller seller)
        {
            var UpdatedSeller = JsonConvert.DeserializeObject<Seller>(await _service.PostMethodWithReturn(seller, $"SellerApi/UpdateSeller", null));
            return View(UpdatedSeller);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSeller(int Id)
        {
            var DeleteSeller = JsonConvert.DeserializeObject<Seller>(await _service.GetMethodWithReturn($"SellerApi/DeleteSeller?Id={Id}"));
            return View(DeleteSeller);
        }
    }
}
