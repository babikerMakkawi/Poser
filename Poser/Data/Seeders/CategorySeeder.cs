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
    public class CategorySeeder
    {
        private static readonly Random random = new Random();
        private static readonly Faker faker = new Faker();

        public static void SeedData(ApplicationDbContext context)
        {
                //string genName = faker.Name.FullName();
                //string genSlug = genName?.ToLower().Replace(" ", "-");

                var categories = new Category[]
                {
                    new Category { Name = "Food" , Slug = SlugMaker("Food")},
                    new Category { Name = "Fresh Drinks" , Slug = SlugMaker("Fresh Drinks")},
                    new Category { Name = "Electronics" , Slug = SlugMaker("Electronics")},
                    new Category { Name = "Clothing" , Slug = SlugMaker("Clothing") },
                    new Category { Name = "Home Appliances" , Slug = SlugMaker("Home Appliances") },
                };

                foreach (var my_category in categories)
                {
                    context.Categories.Add(my_category);
                }
                context.SaveChanges();
        }

        public static string SlugMaker(string name) => name.ToLower().Replace(" ", "-");

    }
}