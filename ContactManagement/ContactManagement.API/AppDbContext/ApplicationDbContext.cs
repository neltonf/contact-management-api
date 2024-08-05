using ContactManagement.API.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.API.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
