using System.ComponentModel.DataAnnotations.Schema;
using TrabajoPracticoP3.Data.Enum;
using TrabajoPracticoP3.Data.Entities;

namespace TrabajoPracticoP3.Data.Models
{
    public class OrderPostDto
    {
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Payment { get; set; }
        public DateTime CreationDate { get; } = DateTime.Now.ToUniversalTime();
    }
}
