using ContactManagement.API.AppDbContext;
using ContactManagement.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await _context.Contacts.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> Post([FromBody] Contact contact)
        {
            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync();

            return await _context.Contacts.FindAsync(contact.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null) return NotFound();

            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] long id, [FromBody] Contact contact)
        {
            if (id != contact.Id) return BadRequest();

            var filteredContact = await _context.Contacts.FindAsync(id);

            if (filteredContact == null) return NotFound();

            _context.Entry(contact).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
