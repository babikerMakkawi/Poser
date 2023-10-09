using Poser.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Poser.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public string? Image { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
