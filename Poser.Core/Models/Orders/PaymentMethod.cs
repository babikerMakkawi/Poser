using System.ComponentModel.DataAnnotations;

namespace Poser.Core.Models.Orders
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = false;
    }
}
