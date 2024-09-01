using DataAccess;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace ElsaYazilim.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        AppDbContext _context = new AppDbContext();
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult SignIn(string name, string password)
        //{
        //    var user = _context.Users.FirstOrDefault(x => x.Name == name && x.Password == password && x.Status == 1);
        //    if (user != null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> SignIn(LoginDto loginDto)
        //{
        //var httpClient = _httpClientFactory.CreateClient();
        //var jsonContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

        //    var response = await httpClient.PostAsync("https://localhost:44322/api/LoginApi/SignIn", jsonContent);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

        //        // Giriş başarılı olduğunda yapılacak işlemler
        //return RedirectToAction("Index", "Home");
        //   }
        //   else
        //    {
        //// Giriş başarısız olduğunda yapılacak işlemler
        //return RedirectToAction("Index", "Login");
        //}
        //}
        [HttpPost]
        public async Task<bool> SignIn(LoginDto loginDto)
        {


            var httpClient = _httpClientFactory.CreateClient();
            // JSON içeriğini kontrol etme
            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44322/api/LoginApi/SignIn", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return true;
            }
            else
            {
                return false;
            }




        }


    }
}
