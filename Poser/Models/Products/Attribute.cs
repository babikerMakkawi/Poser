using System.ComponentModel.DataAnnotations;

namespace Poser.Models.Products
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
    }
}
