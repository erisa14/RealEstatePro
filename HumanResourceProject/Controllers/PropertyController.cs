using DAL.Contracts;
using Domain.Contracts;
using DTO;
using DTO.Property;
using Entities;
using Entities.Models;
using Helpers.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace RealEstatePro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyDomain _propertyDomain;

        public PropertyController(IPropertyDomain propertyDomain)
        {
            _propertyDomain = propertyDomain;
        }


        private static List<Property> properties = new List<Property>();


        [HttpGet]
        [Route("getAllProperties")]
        public IActionResult GetAllProperties()
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var properties = _propertyDomain.GetAllProperties();

                if (properties != null)
                {
                    return Ok(properties);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }



        [HttpGet]
        [Route("{propertyId}")]
        public IActionResult GetPropertyById([FromRoute] Guid propertyId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var property = _propertyDomain.GetPropertyById(propertyId);

                if (property != null)
                    return Ok(property);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet]
        [Route("getAllPropertiesByCategory/{categoryName}")]
        public async Task<ActionResult<List<Property>>> GetAllPropertiesByCategory(CategoryEnum.CategoryName categoryName)
        {

           var properties= await _propertyDomain.GetPropertyByCategory(categoryName);
            return properties;
        }



        [HttpGet]
        [Route("getAllPropertiesByPrice")]
        public async Task<ActionResult<List<Property>>> GetAllPropertiesByPrice(decimal minPrice, decimal maxPrice)

        { 
            var properties =await _propertyDomain.GetAllPropertiesByPrice(minPrice, maxPrice);
            return properties;


           
        }

        /*
        [HttpPost]
        [Route("deleteProperty")]
        public IActionResult DeletePropertyById([FromRoute] Guid propertyId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var propertyToDelete = _propertyDomain.GetPropertyById(propertyId);

                if (propertyToDelete != null)
                    return Ok(propertyToDelete);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        */


        
        [HttpDelete]
        [Route("deleteProperty")]
        public async Task<IActionResult> DeleteProperty(Guid PropertyId)
        {
           await _propertyDomain.DeleteProperty(PropertyId);
            return Ok();
        }


        [HttpPost]
        [Route("addProperty")]
        public async Task<IActionResult> AddProperty([FromBody]PropertyDTO propertyDTO)
        {
            try
            {
                await _propertyDomain.AddProperty(propertyDTO);
                return Ok("Property registered!");
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }



       
        
        [HttpPost]
        [Route("editProperty")]
        public IActionResult EditProperty(EditPropertyDTO propertyDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _propertyDomain.EditProperty(propertyDTO);
                    return Ok(result);
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return BadRequest("Invalid property data");
        }
        

        [HttpPost]
        [Route("uploadFile")]
        public Response UploadFile([FromForm]FileModel fileModel)
        {
            Response response = new Response();
            try
            {
                string path = Path.Combine(@"C:\\Users\\User\\Desktop\\New folder (2)", fileModel.FileName);
                using(Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                }
                response.StatusCode = 200;
                response.ErrorMessage="Image created succesfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.ErrorMessage="Some error occurred"+ ex.Message;
            }
            return response;

        }
    }
}




