using System;
using System.ComponentModel.DataAnnotations;


namespace TrabajoPracticoP3.Data.Models
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}

