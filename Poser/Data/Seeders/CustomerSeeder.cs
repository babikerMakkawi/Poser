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
    public class CustomerSeeder
    {
        private static readonly Random random = new Random();
        private static readonly Faker faker = new Faker();

        public static void SeedData(ApplicationDbContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                var customer = new Customer
                {
                    Name = faker.Name.FullName(),
                    Email = faker.Internet.Email(),
                    PhoneNumber = faker.Lorem.Paragraph(),
                    Address = faker.Address.FullAddress(),
                };
                context.Customers.Add(customer);
            }
            try
            {
            context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}