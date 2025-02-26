using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrabajoPracticoP3.Data.Enum;

namespace TrabajoPracticoP3.Data.Entities
{
    public class Order
    {
        internal object ProductId;
        internal object Quantity;
        internal object TotalPrice;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Payment { get; set; }
        public DateTime CreationDate { get; } = DateTime.Now.ToUniversalTime();
        public string? TotalPrize { get; set; }

        public OrderState State { get; set; } = OrderState.Pending;

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }
        public int ClientId { get; set; }

        public List<SaleOrderLine> SaleOrderLines { get; set; } = new();
        public int IdOrder { get; internal set; }
    }

}