using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poser.Models.Products
{
    public class AttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        public Models.Products.Attribute Attribute { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;
    }
}