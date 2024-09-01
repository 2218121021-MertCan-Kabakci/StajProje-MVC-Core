using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        private AppDbContext _context = new AppDbContext();
        [HttpGet("GetContactInfo")]
        public List<Contact> GetContactInfo()
        {
            List<Contact> contacts = _context.Contacts.ToList();

            return contacts;
        }

        [HttpPost("AddContact")]
        public bool AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return true;

        }
        [HttpPost("UpdateContact")]
        public IActionResult UpdateContact(Contact contact)
        {
            if (contact == null && contact.Id == 0)
            {
                return BadRequest();
            }
            var UpdatedContact = _context.Contacts.Find(contact.Id);
            if (UpdatedContact == null)
            {
                return BadRequest();
            }
            UpdatedContact.Adress = contact.Adress;
            UpdatedContact.HomePhone = contact.HomePhone;
            UpdatedContact.OfficePhone = contact.OfficePhone;
            UpdatedContact.FirstPhone = contact.FirstPhone;
            UpdatedContact.SecondPhone = contact.SecondPhone;
            UpdatedContact.Email = contact.Email;
            UpdatedContact.Id = contact.Id;
            _context.Contacts.Add(UpdatedContact);
            _context.SaveChanges();
            return Ok();

        }
        [HttpGet("DeleteContact")]
        public IActionResult DeleteContact(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.Status = 2;
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
    }
}
