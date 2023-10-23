using Microsoft.AspNetCore.Mvc;
using Poser.Data;
using Poser.Models;

namespace Poser.Controllers
{
    [Route("Pos")]
    public class PosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PosController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var compositeModel = new CompositeModel
            {
                Products = _context.Products.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View("../Dashboard/Pos/index", compositeModel);
        }
    }
}
