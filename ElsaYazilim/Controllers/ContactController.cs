using DataAccess;
using ElsaYazilim.Helpers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElsaYazilim.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Service _service;

        public ContactController(IHttpClientFactory httpClientFactory, Service service)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
        }

        AppDbContext _context = new AppDbContext();
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/ContactApi/GetCategoryInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Contact>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetContactInfo()
        {

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/ContactApi/GetCategoryInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Contact>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(contact, $"ContactApi/AddContact", null));
            return Json(new { succses = isAdded });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Contact contact)
        {
            var UpdatedCategory = JsonConvert.DeserializeObject<Contact>(await _service.PostMethodWithReturn(contact, $"ContactApi/UpdateContact", null));
            return View(UpdatedCategory);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var DeleteProduct = JsonConvert.DeserializeObject<Contact>(await _service.GetMethodWithReturn($"ContactApi/DeleteContact?Id={Id}"));
            return View(DeleteProduct);
        }
    }
}
