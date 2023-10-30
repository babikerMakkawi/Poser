using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poser.Core.Models.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        //public int UserId { get; set; } //Shift User Employee
        public int? PaymentMethodId { get; set; }

        [Required]
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }

        public double Total { get; set; }

        public bool IsPaid { get; set; } = true;

        [Display(Name = "PCustomer")]
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer{ get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}