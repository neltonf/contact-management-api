using ContactManagement.API.AppDbContext;
using ContactManagement.API.Model;
using ContactManagement.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts([FromQuery] SearchParams searchParams)
        {
            IQueryable<Contact> filtered = _context.Contacts;

            if (string.IsNullOrWhiteSpace(searchParams.Search))
                filtered = filtered
                    .Take(searchParams.Size)
                    .Skip(searchParams.Size * searchParams.Page);
            else
                filtered = filtered
                        .Where(x =>
                            x.FirstName.Contains(searchParams.Search) ||
                            x.LastName.Contains(searchParams.Search) ||
                            x.Email.Contains(searchParams.Search));

            return filtered.ToList();
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
