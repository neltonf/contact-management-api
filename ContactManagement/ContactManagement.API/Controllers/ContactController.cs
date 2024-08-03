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

        [HttpGet("Contacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> Index()
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
    }
}
