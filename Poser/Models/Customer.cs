using Poser.Models.Orders;
using Poser.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Poser.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Customer name can't be longer than 100 characters.")]
        public string Name { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email address can't be longer than 100 characters.")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, ErrorMessage = "Phone number can't be longer than 20 characters.")]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Address can't be longer than 255 characters.")]
        public string Address { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
