﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poser.Core.Models.Products
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Attribute Name")]
        public string Name { get; set; } = null!;
        public ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
    }
}