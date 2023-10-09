using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poser.Models.Products
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int ProductStockId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [ForeignKey("ProductStockId")]
        public ProductStock? ProductStock { get; set; }

        [ForeignKey("AttributeId")]
        public Models.Products.Attribute? Attribute { get; set; }

        [ForeignKey("AttributeValueId")]
        public AttributeValue? AttributeValue { get; set; }

        //1 (Polo Frenzy #12432c T-Shirt), 1, [1 color,1 red]
        //1 (Polo Frenzy #12432c T-Shirt), 1, [2 size ,1 xl]
    }
}
