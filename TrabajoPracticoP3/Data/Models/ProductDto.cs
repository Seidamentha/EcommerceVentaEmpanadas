using System;
using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoP3.Data.Models
{
	public class ProductDto
	{
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? NameProduct { get; set; }


    }
}

