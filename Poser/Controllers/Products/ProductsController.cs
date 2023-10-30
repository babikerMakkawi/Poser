using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Poser.Core.Models.Products;
using Poser.EF;

namespace Poser.Controllers.Products
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("../Dashboard/Products/index");
        }

        //Create Product Partial Modal
        [HttpGet]
        [Route("CreateProduct/")]
        public IActionResult CreateAttribute()
        {
            try
            {

                ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name"); 
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name"); 

                return PartialView("../Dashboard/Products/Partials/_CreateProductPartial");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        //Create Product Action
        [HttpPost]
        [Route("CreateAction")]
        public ActionResult CreateAction(Product product)
        {
            //if (product.Name == null)
            //{
            //    return Json(new { success = false, message = "Product Name Is Required!" });
            //}
            //if (_context.Products.Where(u => u.Name == product.Name).FirstOrDefault() != null)
            //{
            //    return Json(new { success = false, message = "This Name is Already in Use!" });
            //}

            _context.Products.Add(product);
            _context.SaveChanges();

            return Json(new { success = true, message = "Product Created Successfully" });
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////


        //Update Product Partial Modal
        [HttpGet]
        [Route("EditProduct/{id}")]
        public IActionResult EditProduct(int id)
        {
            try
            {
                ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name"); // Pass the categories to the view
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name"); // Pass the categories to the view

                var product =_context.Products
                        .Include(a => a.ProductStocks)
                        .Include(b => b.Category)
                        .Include(b => b.Brand)
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

                return PartialView("../Dashboard/Products/Partials/_EditProductPartial", product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //Create Product Action
        [HttpPost("UpdateAction")]
        public ActionResult UpdateAction(Poser.Core.Models.Products.Product product)
        {
            //if (product.Name == null)
            //{
            //    return Json(new { success = false, message = "Please Fill The Fields!" });
            //}

            //if (_context.Attributes.AsNoTracking().SingleOrDefault(a => a.Name == product.Name) != null)
            //{
            //    return Json(new { success = false, message = "This Name is Already in Use!" });
            //}

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Json(new { success = true, message = "Product Updated successfully" });
        }



























        [HttpGet]
        [Route("GetJsonData")]
        public async Task<JsonResult> GetJsonData()
        {
            var data = await _context.Products
                        .Include(b => b.Category)
                        .Include(b => b.Brand)
                        .ToListAsync();

            return (data != null ) ? Json(data) : Json(new { success = false, message = "Data Not Fetched Succesfully" });

            //try
            //{
            //    return Json(
            //        _context.Products
            //            .Include(b => b.ProductStocks)
            //            .Include(b => b.Category)
            //            .Include(b => b.Brand)
            //            .ToList());
            //}
            //catch (Exception ex)
            //{
            //    return Json(ex);
            //}
        }

    }
}

/*
 * [Route("Products/Attributes")]
    public class AttributesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("../Dashboard/Attributes/index");
        }

        [HttpGet]
        [Route("CreateAttribute/")]
        public IActionResult CreateAttribute()
        {
            try
            {
                return PartialView("../Dashboard/Attributes/Partials/_CreateAttributePartial");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost]
        [Route("CreateAction")]
        public ActionResult CreateAction(Models.Products.Attribute attribute)
        {
            if (attribute.Name == null)
            {
                return Json(new { success = false, message = "Attribute Name Is Required!" });
            }
            if (_context.Attributes.Where(u => u.Name == attribute.Name).FirstOrDefault() != null)
            {
                return Json(new { success = false, message = "This Name is Already in Use!" });
            }

            _context.Attributes.Add(attribute);
            _context.SaveChanges();

            return Json(new { success = true, message = "Attribute Created Successfully" });

            //ModelState.AddModelError(nameof(Models.Products.Attribute.Name), "This Name is Already in Use.");
        }







        [HttpGet]
        [Route("EditAttribute/{id}")]
        public IActionResult EditAttribute(int id)
        {
            try
            {
                var attribute = _context.Attributes
                    .Include(a => a.AttributeValues)
                    .Where(a => a.Id == id)
                    .FirstOrDefault();

                return PartialView("../Dashboard/Attributes/Partials/_EditAttributePartial", attribute);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost("UpdateAction")]
        public ActionResult UpdateAction(Models.Products.Attribute attribute)
        {
            if (attribute.Name == null)
            {
                return Json(new { success = false, message = "Please Fill The Fields!" });
            }

            if (_context.Attributes.AsNoTracking().SingleOrDefault(a => a.Name == attribute.Name) != null)
            {
                return Json(new { success = false, message = "This Name is Already in Use!" });
            }

            _context.Entry(attribute).State = EntityState.Modified;
            _context.SaveChanges();

            return Json(new { success = true, message = "Attribute Updated successfully" });
        }








        [HttpGet]
        [Route("DeleteAttribute/{id}")]
        public IActionResult DeleteAttribute(int id)
        {
            try
            {
                return PartialView("../Dashboard/Attributes/Partials/_DeleteAttributePartial", _context.Attributes.Find(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);

            if (attribute == null)
            {
                return Json(new { success = false, message = "Attribute Not Found!" });
            }

            _context.Attributes.Remove(attribute);
            _context.SaveChanges();

            return  Json(new { success = true, message = "Attribute Deleted Successfully" });
        }



        //Partials Actions
        [HttpGet]
        [Route("GetJsonData")]
        public JsonResult GetJsonData()
        {
            try
            {
                return Json(_context.Attributes
                    .Include(a => a.AttributeValues)
                    .ToList());
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

    }
*/