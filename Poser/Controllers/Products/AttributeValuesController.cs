using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poser.EF;

namespace Poser.Controllers.Products
{
    [Route("Products/AttributeValues")]
    public class AttributeValusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributeValusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CreateAttributeValue/{id?}")]
        public IActionResult CreateAttributeValue(int id)
        {
            try
            {
                ViewData["attribute_id"] = id;
                return PartialView("../Dashboard/Attributes/Partials/AttributeValues/_CreateAttributeValuePartial");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost]
        [Route("CreateAction")]
        public ActionResult CreateAction(Poser.Core.Models.Products.AttributeValue attributeValue)
        {
            if (attributeValue.Name == null)
            {
                return Json(new { success = false, message = "Attribute Value Name Is Required!" });
            }
            if (
                _context.AttributeValues
                .Where(u => u.Name == attributeValue.Name)
                .Where(u => u.AttributeId == attributeValue.AttributeId)
                .FirstOrDefault() != null
                )
            {
                return Json(new { success = false, message = "Attribute Value is Already in Use With this Attribute" });
            }

            _context.AttributeValues.Add(attributeValue);
            _context.SaveChanges();

            return Json(new { success = true, message = "Attribute Value Created Successfully" });
        }






        [HttpGet]
        [Route("EditAttributeValue/{id}")]
        public IActionResult EditAttributeValue(int id)
        {
            try
            {
                var AttributeValue = _context.AttributeValues
                    .Where(a => a.Id == id)
                    .FirstOrDefault();

                return PartialView("../Dashboard/Attributes/Partials/AttributeValues/_EditAttributeValuePartial", AttributeValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost("UpdateAction")]
        public ActionResult UpdateAction(Poser.Core.Models.Products.AttributeValue attributeValue)
        {
            if (attributeValue.Name == null)
            {
                return Json(new { success = false, message = "Please Fill The Fields!" });
            }

            if (
                _context.AttributeValues
                .Where(u => u.Name == attributeValue.Name)
                .Where(u => u.AttributeId == attributeValue.AttributeId)
                .FirstOrDefault() 
                != null
                )
            {
                return Json(new { success = false, message = "Attribute Value is Already in Use With this Attribute!" });
            }


            _context.Entry(attributeValue).State = EntityState.Modified;
            _context.SaveChanges();

            return Json(new { success = true, message = "Attribute Value Updated successfully" });
        }




        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var attributeValue = await _context.AttributeValues.FindAsync(id);

            if (attributeValue == null)
            {
                return Json(new { success = false, message = "Attribute Value Not Found!" });
            }

            _context.AttributeValues.Remove(attributeValue);
            _context.SaveChanges();

            return Json(new { success = true, message = "Attribute Value Deleted Successfully" });
        }



        //Partials Actions
        [HttpGet]
        [Route("GetJsonData")]
        public JsonResult GetJsonData()
        {
            try
            {
                return Json(_context.AttributeValues.ToList());
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

    }
}
