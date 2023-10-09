using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poser.Data;

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
            Data.Seeders.CustomerSeeder.SeedData(_context);
        }
    }
}
