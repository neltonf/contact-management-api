using ContactManagement.API.Model;
using ContactManagement.API.Models;
using ContactManagement.API.Services.FileService;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IFileService _fileService;

        public ContactController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult GetContacts([FromQuery] SearchParams searchParams)
        {
            IEnumerable<Contact> filtered = _fileService.GetContacts();
            
            if (!string.IsNullOrEmpty(searchParams.Search))
            {
                filtered = filtered.Where(x =>
                            x.FirstName.ToLower().Contains(searchParams.Search.ToLower()) ||
                            x.LastName.ToLower().Contains(searchParams.Search.ToLower()) ||
                            x.Email.ToLower().Contains(searchParams.Search.ToLower()));
            }

            var result = new
            {
                Data = filtered.Skip(searchParams.Size * searchParams.Page).Take(searchParams.Size).ToList(),
                Length = filtered.Count()
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        {
            _fileService.AddContact(contact);

            return Ok(_fileService.GetContact(contact.Id));
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] long id)
        {
            _fileService.DeleteContact(id);

            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([FromQuery] long id, [FromBody] Contact contact)
        {
            if (id != contact.Id) return BadRequest();

            _fileService.UpdateContact(contact);

            return NoContent();
        }
    }
}
