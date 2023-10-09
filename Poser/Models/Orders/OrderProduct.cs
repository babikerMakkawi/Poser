using Poser.Models;
using Poser.Models.Orders;
using Poser.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poser.Models.Orders
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductStockId { get; set; }

        [Required]
        [ForeignKey("ProductStockId")]
        public ProductStock ProductStock { get; set; } = null!;

        [Required]
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        public int Quantity{ get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
