using DataAccess;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoginApiController(AppDbContext context)
        {
            _context = context;
        }
        #region Login Giriş İşlemleri
        
        [HttpPost("SignIn")]
        public IActionResult SignIn(LoginDto us) //tek parametre alır....
        {
            var user = _context.Users.FirstOrDefault(x => x.Name == us.Name && x.Password == us.Password);
            if (user != null)
            {
                {
                    return Ok(new {Message = "Giriş Başarılı", User = user});
                }
            }
            else
            {
                return Unauthorized(new { Message = "Geçersiz Kullanıcı" });
            }
        }
        #endregion
    }
}
