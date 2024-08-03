using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.API.Model
{
    public class Contact
    {
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
