using Poser.Data;
using Poser.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using Bogus;
using Poser.Core.Models.Products;
using Poser.Core.Models;
using Poser.EF;

namespace Poser.Data.Seeders
{
    public class ProductSeeder
    {
        private static readonly Random random = new Random();
        private static readonly Faker faker = new Faker();

        public static void SeedData(ApplicationDbContext context)
        {
            for (var i = 0; i < 20; i++)
            {
                var product = new Product
                {
                    Name = faker.Name.FullName(),
                    Image = "https://source.unsplash.com/300x400/?book&img=" + faker.Random.Int(1, 1000),
                    Description = faker.Lorem.Paragraph(),
                    BrandId = faker.Random.Int(1, 5),
                    CategoryId = faker.Random.Int(1, 5),
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
//  VersionNumber = faker.Random.Words(1),
//  Price = faker.Random.Double(4.99, 99.99),