using System.ComponentModel.DataAnnotations;
using Poser.Core.Models.Products;
using Poser.Core.Models;

namespace Poser.Core.Models
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
