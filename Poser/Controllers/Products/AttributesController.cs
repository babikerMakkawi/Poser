﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poser.Core.Interfaces;
using Poser.Core;
using Poser.EF;

namespace Poser.Controllers.Products
{
    [Route("Products/Attributes")]
    public class AttributesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AttributesController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
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
        public ActionResult CreateAction(Poser.Core.Models.Products.Attribute attribute)
        {
            if (attribute.Name == null)
            {
                return Json(new { success = false, message = "Attribute Name Is Required!" });
            }

            if (_unitOfWork.Attributes.FindAsNoTracking(a => a.Name == attribute.Name) != null)
            {
                return Json(new { success = false, message = "This Name is Already in Use!" });
            }

            _unitOfWork.Attributes.Add(attribute);
            _unitOfWork.complete();

            return Json(new { success = true, message = "Attribute Created Successfully" });
        }







        [HttpGet]
        [Route("EditAttribute/{id}")]
        public IActionResult EditAttribute(int id)
        {
            try
            {
                var attribute = _unitOfWork.Attributes.Find(a => a.Id == id, new []{ "AttributeValues" });

                return PartialView("../Dashboard/Attributes/Partials/_EditAttributePartial", attribute);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost("UpdateAction")]
        public ActionResult UpdateAction(Poser.Core.Models.Products.Attribute attribute)
        {
            if (attribute.Name == null)
            {
                return Json(new { success = false, message = "Please Fill The Fields!" });
            }

            if(_unitOfWork.Attributes.FindAsNoTracking(a => a.Name == attribute.Name) != null)
            {
                return Json(new { success = false, message = "This Name is Already in Use!" });
            }

            _unitOfWork.Attributes.Update(attribute);
            _unitOfWork.complete();

            return Json(new { success = true, message = "Attribute Updated successfully" });
        }








        [HttpGet]
        [Route("DeleteAttribute/{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            try
            {
                var attribute = await _unitOfWork.Attributes.FindAsync(a => a.Id == id);
                return PartialView("../Dashboard/Attributes/Partials/_DeleteAttributePartial", attribute);
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
            var attribute = await _unitOfWork.Attributes.FindAsync(a => a.Id == id);

            if (attribute == null)
            {
                return Json(new { success = false, message = "Attribute Not Found!" });
            }

            _unitOfWork.Attributes.Delete(attribute);
            _unitOfWork.complete();
            
            return  Json(new { success = true, message = "Attribute Deleted Successfully" });
        }



        //Partials Actions
        [HttpGet]
        [Route("GetJsonData")]
        public async Task<JsonResult> GetJsonData()
        {
            try
            {
                var attributes = await _unitOfWork.Attributes.FindAllAsync(new[] {"AttributeValues"});

                return Json(attributes);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

    }
}