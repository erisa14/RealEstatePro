﻿using DAL.Contracts;
using Domain.Contracts;
using DTO.PropertyDTO;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("getAllPropertiesByCategory")]
        public IActionResult GetAllPropertiesByCategory()

        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var properties = _propertyDomain.GetAllPropertiesByCategory();

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
        [Route("getAllPropertiesByPrice")]
        public IActionResult GetAllPropertiesByPrice(decimal minPrice, decimal maxPrice)

        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var property = _propertyDomain.GetAllPropertiesByPrice(2, 3);

                if (property != null)
                {
                    return Ok(property);
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

        [HttpPost]
        [Route("addProperty")]
        public IActionResult AddProperty([FromRoute] Guid propertyId)
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
    }
}





