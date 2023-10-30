using Bogus;
using Poser.EF;
using Poser.Core.Models;

namespace Poser.Data.Seeders
{
    public class CustomerSeeder
    {
        private static readonly Faker faker = new Faker();

        public static void SeedData(ApplicationDbContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                var customer = new Customer
                {
                    Name = faker.Name.FullName(),
                    Email = faker.Internet.Email(),
                    PhoneNumber = faker.Phone.PhoneNumber("05########"),
                    Address = faker.Address.FullAddress(),
                };
                context.Customers.Add(customer);
            }
            context.SaveChanges();
        }
    }
}