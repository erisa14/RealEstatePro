﻿using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.PropertyDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class PropertyDomain : DomainBase, IPropertyDomain
    {
        public PropertyDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IPropertyRepository propertyRepository => _unitOfWork.GetRepository<IPropertyRepository>();
        public IList<PropertyDTO> GetAllProperties()
        {
            IEnumerable<Property> property = propertyRepository.GetAll();
            var allProperties = _mapper.Map<IList<PropertyDTO>>(property);
            return allProperties;
        }

        public PropertyDTO GetPropertyById(Guid id)
        {
            Property property = propertyRepository.GetById(id);
            return _mapper.Map<PropertyDTO>(property);
        }

        public IList<PropertyDTO> GetAllPropertiesByCategory()
        {
            IEnumerable<Property> property = propertyRepository.GetAll();
            var allPropertiesByCategory = _mapper.Map<IList<PropertyDTO>>(property);
            return allPropertiesByCategory;
        }

        public IList<PropertyDTO> GetAllPropertiesByPrice(decimal minPrice, decimal maxPrice)
        {
            // Get all properties from the repository
            IEnumerable<Property> property = propertyRepository.GetAll();

            // Filter properties based on price range
            property = property.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

            // Map the filtered properties to DTOs
            var propertyDTOs = _mapper.Map<IList<PropertyDTO>>(property);

            return propertyDTOs;
        }

        public PropertyDTO AddProperty(PropertyDTO property)
        {
            throw new NotImplementedException();
        }
    }
}