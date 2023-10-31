using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Poser.Core.Interfaces;
using Poser.Core.Models.Products;
using Poser.EF;
using System.Linq.Expressions;

namespace Poser.Controllers.Products
{
    [Route("Products/AttributeValues")]
    public class AttributeValusController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public AttributeValusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Create AttributeValue Partial
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

        //Create AttributeValue Action
        [HttpPost]
        [Route("CreateAction")]
        public async Task<ActionResult> CreateAction(Poser.Core.Models.Products.AttributeValue attributeValue)
        {
            if (attributeValue.Name == null)
            {
                return Json(new { success = false, message = "Attribute Value Name Is Required!" });
            }

            var criteriaArray = new Expression<Func<AttributeValue, bool>>[]
            {
                u => u.Name == attributeValue.Name,
                u => u.AttributeId == attributeValue.AttributeId
            };

            if (await _unitOfWork.AttributeValues.FindAsync(criteriaArray) != null)
            {
                return Json(new { success = false, message = "Attribute Value is Already in Use With this Attribute" });
            }

            await _unitOfWork.AttributeValues.AddAsync(attributeValue);
            _unitOfWork.complete();


            return Json(new { success = true, message = "Attribute Value Created Successfully" });
        }



        //Update AttributeValue Partial
        [HttpGet]
        [Route("EditAttributeValue/{id}")]
        public async Task<IActionResult> EditAttributeValue(int id)
        {
            try
            {
                var AttributeValue = await _unitOfWork.AttributeValues.FindAsync(a => a.Id == id);

                return PartialView("../Dashboard/Attributes/Partials/AttributeValues/_EditAttributeValuePartial", AttributeValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        //Update AttributeValue Action
        [HttpPost("UpdateAction")]
        public async Task<ActionResult> UpdateAction(Poser.Core.Models.Products.AttributeValue attributeValue)
        {
            if (attributeValue.Name == null)
            {
                return Json(new { success = false, message = "Please Fill The Fields!" });
            }

            var criteriaArray = new Expression<Func<AttributeValue, bool>>[]
            {
                u => u.Name == attributeValue.Name,
                u => u.AttributeId == attributeValue.AttributeId
            };

            if (await _unitOfWork.AttributeValues.FindAsync(criteriaArray) != null)
            {
                return Json(new { success = false, message = "Attribute Value is Already in Use With this Attribute!" });
            }

            _unitOfWork.AttributeValues.Update(attributeValue);
            _unitOfWork.complete();

            return Json(new { success = true, message = "Attribute Value Updated successfully" });
        }



        //Delete AttributeValue Action
        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var attributeValue = await _unitOfWork.AttributeValues.FindAsync(av => av.Id == id);

            if (attributeValue == null)
            {
                return Json(new { success = false, message = "Attribute Value Not Found!" });
            }


            _unitOfWork.AttributeValues.Delete(attributeValue);
            _unitOfWork.complete();

            return Json(new { success = true, message = "Attribute Value Deleted Successfully" });
        }



        //AttributeValues Data
        [HttpGet]
        [Route("GetJsonData/{id}")]
        public async Task<JsonResult> GetJsonData(int id)
        {
            try
            {
                var asdasd = id;
                var attributeValues = await _unitOfWork.AttributeValues.FindAllAsync(av => av.AttributeId == id);

                return Json(attributeValues);

                //return (attributeValues == null)
                //    ? Json(new { success = false, message = "Attribute Values Not Found!" })
                //    : Json(attributeValues);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

    }
}
