using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HomeApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();


        #region Kullanıcı Listeleme İşlemleri
        [HttpGet("GetUsers")]
        public List<User> GetUsers(string name, string surname)
        {
            List<User> users = _context.Users.ToList();

            return users;
        }
        #endregion

        #region Rol İşlemleri
        [HttpGet("GetRolInfo")]
        public List<Rol> GetRolInfo()
        {
            List<Rol> rols = _context.Rols.ToList();

            return rols;
        }
        [HttpPost("AddRol")]
        public async Task<bool> AddRol(Rol rol)
        {
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();
            return true;

        }
        [HttpPost("UpdateRol")]
        public IActionResult UpdateRol(Rol rol)
        {
            if (rol == null && rol.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedRol = _context.Rols.Find(rol.Id);
            if (UpdatedRol == null)
            {
                return BadRequest();
            }
            UpdatedRol.Id = rol.Id;
            UpdatedRol.Name = rol.Name;
            _context.Rols.Update(UpdatedRol);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("DeleteRol")]
        public IActionResult DeleteRol(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var Rol = _context.Rols.Find(id);
            if (Rol == null)
            {
                return NotFound();
            }

            _context.Rols.Update(Rol);
            _context.SaveChanges();
            return Ok(Rol);
        }
        #endregion
        [HttpGet("GetUserInfo")]
        public List<User> GetUserInfo()
        {
            List<User> users = _context.Users.ToList();

            return users;
        }
        [HttpPost("AddUser")]
        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;

        }
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            if (user == null && user.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedUser = _context.Users.Find(user.Id);
            if (UpdatedUser == null)
            {
                return BadRequest();
            }
            UpdatedUser.Id = user.Id;
            UpdatedUser.RolId = user.RolId;
            UpdatedUser.Email = user.Email;
            UpdatedUser.Status = user.Status;
            UpdatedUser.Password = user.Password;
            _context.Users.Update(UpdatedUser);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var users = _context.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            users.Status = 2;
            _context.Users.Update(users);
            _context.SaveChanges();
            return Ok(users);
        }
    }

}
