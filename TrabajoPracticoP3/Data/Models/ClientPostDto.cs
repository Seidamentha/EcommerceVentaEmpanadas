using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoP3.Data.Models
{
    public class ClientPostDto
    {
        [Required]
        public string Name { get; set; }
        public string? SurName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string? UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string?Adress { get; set;}
    }
}
