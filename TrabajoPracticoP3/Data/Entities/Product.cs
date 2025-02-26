using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.Data.Enum;


namespace TrabajoPracticoP3.Data.Entities
{
    public class Product
    {
        internal int IdProduct;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public string? Name { get; set; }

        
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool State { get; set; } = true;
        public ProductState ProductState { get; set; } = ProductState.Active;
        public ICollection<SaleOrderLine> SaleOrderLines { get; set; } = new List<SaleOrderLine>();
    }
}
