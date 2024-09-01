using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public ProductController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }
        AppDbContext _context = new AppDbContext();
        public IActionResult Index()
        {
            return View();
        }

        #region Ürün Listeleme İşlemleri
        [HttpGet]
        public async Task<IActionResult> GetProductInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/ProductApi/GetProductInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Product>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        #endregion

        #region Ürün Ekleme İşlemleri
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(product, $"ProductApi/AddProduct", null));
            return Json(new { succses = isAdded });
        }
        #endregion

        #region Ürün Güncelleme İşlemleri
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var UpdatedProduct = JsonConvert.DeserializeObject<Product>(await _service.PostMethodWithReturn(product, $"ProductApi/UpdateProduct", null));
            return View(UpdatedProduct);
        }
        #endregion

        #region Ürün Silme İşlemleri
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var DeleteProduct = JsonConvert.DeserializeObject<Product>(await _service.GetMethodWithReturn($"ProductApi/DeleteProduct?Id={Id}"));
            return View(DeleteProduct);
        }
        #endregion
    }
}
