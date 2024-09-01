using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public CustomerController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }
        AppDbContext _context = new AppDbContext();
        public IActionResult Index()
        {
            return View();
        }
        #region Müşteri Bilgileri İşlemleri
        [HttpGet]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/CustomerApi/GetCustomer");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        #endregion

        #region Müşteri Ekleme İşlemleri
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            try
            {
                bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(customer, $"CustomerApi/AddCustomer", null));
                return Json(new { success = isAdded });
            }
            catch (Exception ex)
            {
                // Hata mesajını görmek için loglama ekleyin veya hata mesajını geri döndürün
                return Json(new { success = false, error = ex.Message });
            }

        }
        #endregion

        #region Müşteri Güncelleme İşlemleri
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            var UpdatedCustomer = JsonConvert.DeserializeObject<Customer>(await _service.PostMethodWithReturn(customer, $"CustomerApi/UpdateCustomer", null));
            return View(UpdatedCustomer);
        }
        #endregion

        #region Müşteri Silme İşlemleri
        [HttpGet]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            var DeleteCustomer = JsonConvert.DeserializeObject<Customer>(await _service.GetMethodWithReturn( $"CustomerApi/DeleteCustomer?Id={Id}"));
            return Json(new { success = DeleteCustomer });
        }
        #endregion
    }
}
