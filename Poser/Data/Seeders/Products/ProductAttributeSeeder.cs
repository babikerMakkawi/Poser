using Bogus;
using Poser.Core.Models.Products;
using Poser.Core.Models;
using Poser.EF;

namespace Poser.Data.Seeders.Products
{
    public class ProductAttributeSeeder
    {
        private static readonly Faker faker = new Faker();
        public static void SeedData(ApplicationDbContext context)
        {
            var products = context.Products.ToList();

            // All Products Loop
            foreach (var product in products)
            {
                // Foreach Attributes [Color - Size - Material]
                foreach (var attribute in context.Attributes.ToList())
                {
                    // Check If Attribute Has Values
                    //if (attribute.AttributeValues.Any() != null) 
                    var attribtueValues = context.AttributeValues.Where(a => a.AttributeId == attribute.Id).ToList();
                    if (attribtueValues != null) 
                    {
                        var AV_Count = attribtueValues.Count();

                        // Loop Over AttributeValues
                        foreach(var attributeValue in attribute.AttributeValues.Take(faker.Random.Int(1, AV_Count)).ToList()) { 
                            var productStock = new ProductStock()
                            {
                                ProductId = product.Id,
                                Sku = faker.Random.Int(5, 50),
                                Price = faker.Random.Double(5.00, 100.00),
                            };
                            
                            context.ProductStocks.Add(productStock);
                        }
                    }
                }//  End Of Attribute Loop

                // Save All Created Product Stocks
                context.SaveChanges();

                // TODO: Create ProductAttributes Foreach ProductStock
                // Create Variations For The Product Stock
                foreach(var productStock in context.ProductStocks.Where(ps => ps.ProductId == product.Id).ToList())
                {
                    foreach(var attribute in context.Attributes.ToList())
                    {
                        var productAttribute = new ProductAttribute()
                        {
                            ProductId = product.Id,
                            ProductStockId = productStock.Id,
                            AttributeId = attribute.Id,
                            AttributeValueId = context.AttributeValues.Where(x => x.AttributeId == attribute.Id).OrderBy(x => Guid.NewGuid()).FirstOrDefault().Id,
                        };

                        context.ProductAttributes.Add(productAttribute);
                    }
                }// End Of ProductStocks Loop !!

                context.SaveChanges();

            }// End Of Products Loop !!

        }
    }
}
