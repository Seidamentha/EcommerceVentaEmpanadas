using System;
using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoP3.Data.Models
{
	public class UserPutDto
	{
		
		
          public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }

    }
	
}

