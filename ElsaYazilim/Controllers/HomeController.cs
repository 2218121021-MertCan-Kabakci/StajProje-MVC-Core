using ElsaYazilim.Helpers;
using ElsaYazilim.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ElsaYazilim.Controllers
{
    public class HomeController : Controller
    {
        
        //    
        private readonly ILogger<HomeController> _logger;
        private Service _service;
        private readonly IHttpClientFactory _httpClientFactory;

      
        public HomeController(ILogger<HomeController> logger, Service service, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    string name = "Sedat";
        //    string surname = "Kurtuldu";
        //    List<User> user = JsonConvert.DeserializeObject<List<User>>(await _service.GetMethodWithReturn($"HomeApi/GetUsers?name={name}&surname={surname}"));

        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> GetRolInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/HomeApi/GetRolInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Rol>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddRol(Rol rol)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(rol, $"HomeApi/AddRol", null));
            return Json(new { succses = isAdded });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRol(Rol rol)
        {
            var UpdatedRol = JsonConvert.DeserializeObject<Rol>(await _service.PostMethodWithReturn(rol, $"HomeApi/UpdateRol", null));
            return View(UpdatedRol);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRol(int Id)
        {
            var DeleteRol = JsonConvert.DeserializeObject<Rol>(await _service.GetMethodWithReturn($"HomeApi/DeleteRol?Id={Id}"));
            return View(DeleteRol);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:44322/api/HomeApi/GetUserInfo");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<User>>(responseContent);
                return View(result);
            }
            else
            {
                return BadRequest("API request failed.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            bool isAdded = JsonConvert.DeserializeObject<bool>(await _service.PostMethodWithReturn(user, $"HomeApi/AddUser", null));
            return Json(new { succses = isAdded });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var UpdatedUser = JsonConvert.DeserializeObject<User>(await _service.PostMethodWithReturn(user, $"HomeApi/UpdateUser", null));
            return View(UpdatedUser);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var DeleteUser = JsonConvert.DeserializeObject<User>(await _service.GetMethodWithReturn($"HomeApi/DeleteUser?Id={Id}"));
            return View(DeleteUser);
        }

    }
}