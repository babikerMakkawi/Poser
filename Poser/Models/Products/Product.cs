using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Poser.Models.Products
{
    public class Product
    {
        [Key]
        [Display(Name = "Product Number")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = null!;

        public string? Slug {get => Name?.ToLower().Replace(" ", "-");}

        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = ".jpg,.jpeg,.png")]
        public string? Image { get; set; }

        public string? Description { get; set; } = null!;

        [Display(Name = "PBrand")]
        public int? BrandId { get; set; }

        [Display(Name = "PCategory")]
        public int? CategoryId { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
    }
}