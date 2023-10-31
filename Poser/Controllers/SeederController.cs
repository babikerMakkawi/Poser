using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poser.EF;

namespace Poser.Controllers
{
    public class SeederController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeederController(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SeedCategories()
        {
            Data.Seeders.CategorySeeder.SeedData(_context);
        }

        public void SeedBrands()
        {
            Data.Seeders.BrandSeeder.SeedData(_context);
        }
        public void SeedProducts()
        {
            Data.Seeders.ProductSeeder.SeedData(_context);
        }
        public void SeedCustomers()
        {
            //Data.Seeders.CustomerSeeder.SeedData(_context);
            Data.Seeders.Products.ProductAttributeSeeder.SeedData(_context);
        }
        public void ReSeedBasicData()
        {
            string[] names = { 
                "Categories",
                "Brands",
                "Orders",
                "Customers",

                "ProductAttributes",

                "AttributeValues",
                "Attributes",

                "ProductStocks",
                "Products",

                "PaymentMethods",
            };

            foreach (string name in names)
            {
                _context.Database.ExecuteSqlRaw($"DELETE FROM {name}; DBCC CHECKIDENT ('{name}', RESEED, 1);");
            }

            Data.Seeders.CategorySeeder.SeedData(_context);
            Data.Seeders.BrandSeeder.SeedData(_context);
            Data.Seeders.ProductSeeder.SeedData(_context);
            Data.Seeders.CustomerSeeder.SeedData(_context);
            Data.Seeders.PaymentMethodSeeder.SeedData(_context);
            Data.Seeders.AttributeSeeder.SeedData(_context);
            Data.Seeders.Products.ProductAttributeSeeder.SeedData (_context);
        }

    }
}
