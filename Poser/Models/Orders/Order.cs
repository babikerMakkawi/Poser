using Poser.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poser.Models.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        //public int UserId { get; set; } //Shift User Employee
        public int? PaymentMethodId { get; set; }

        [Required]
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod? PaymentMethod { get; set; }

        public double Total { get; set; }

        public bool IsPaid { get; set; } = true;
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}