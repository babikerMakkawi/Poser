using Poser.Data;
using Poser.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using Bogus;
using Poser.Models.Products;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Poser.Data.Seeders
{
    public class BrandSeeder
    {
        private static readonly Random random = new Random();
        private static readonly Faker faker = new Faker();

        public static void SeedData(ApplicationDbContext context)
        {
            var brands = new Brand[]
            {
                    new Brand { Name = "Almarai" , Slug = SlugMaker("Almarai")},
                    new Brand { Name = "Merinda" , Slug = SlugMaker("Merinda")},
                    new Brand { Name = "Indomi" , Slug = SlugMaker("Indomi")},
                    new Brand { Name = "Nescafe" , Slug = SlugMaker("Nescafe") },
                    new Brand { Name = "Lipton" , Slug = SlugMaker("Lipton") },
            };

            foreach (var my_brands in brands)
            {
                context.Brands.Add(my_brands);
            }
            context.SaveChanges();
        }

        public static string SlugMaker(string name) => name.ToLower().Replace(" ", "-");

    }
}