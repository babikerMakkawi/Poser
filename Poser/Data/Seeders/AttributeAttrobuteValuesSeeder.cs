using Poser.EF;
using Poser.Core.Models;

namespace Poser.Data.Seeders
{
    public class AttributeSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            var attributes = new Core.Models.Products.Attribute[]
            {
                new Core.Models.Products.Attribute(){
                    Name = "Color",
                    AttributeValues = new Core.Models.Products.AttributeValue[]{
                        new Core.Models.Products.AttributeValue(){Name = "Red"},
                        new Core.Models.Products.AttributeValue(){Name = "Green"},
                        new Core.Models.Products.AttributeValue(){Name = "Blue"},
                    }
                },
                new Core.Models.Products.Attribute(){
                    Name = "Size",
                    AttributeValues = new Core.Models.Products.AttributeValue[]{
                        new Core.Models.Products.AttributeValue(){Name = "XL"},
                        new Core.Models.Products.AttributeValue(){Name = "XXL"},
                        new Core.Models.Products.AttributeValue(){Name = "XXXL"},
                    }
                },
                new Core.Models.Products.Attribute(){
                    Name = "Material",
                    AttributeValues = new Core.Models.Products.AttributeValue[]{
                        new Core.Models.Products.AttributeValue(){Name = "Cotton"},
                        new Core.Models.Products.AttributeValue(){Name = "Leather"},
                        new Core.Models.Products.AttributeValue(){Name = "Polyester"},
                    }
                },
            };


            foreach (var attribute in attributes)
            {
                context.Attributes.AddRange(attribute);
            }
            context.SaveChanges();
        }

    }
}
