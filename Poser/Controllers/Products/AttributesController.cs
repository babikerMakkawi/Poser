using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Poser.Data;
using Poser.Models.Products;

namespace Poser.Controllers.Products
{
    [Route("Products/Attributes")]
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



        private bool AttributeExists(int id)
        {
          return _context.Attributes.Any(e => e.Id == id);
        }

    }
}


/*
        // GET: Attributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // GET: Attributes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Models.Products.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }
            return View(attribute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Models.Products.Attribute attribute)
        {
            if (id != attribute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeExists(attribute.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attributes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Attributes'  is null.");
            }
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute != null)
            {
                _context.Attributes.Remove(attribute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        */
