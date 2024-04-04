using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.Property;
using Entities.Models;
using Helpers.Enumerations;
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

        public async Task<List<Property>> GetAllPropertiesByPrice(decimal minPrice, decimal maxPrice)
        {
          return await propertyRepository.GetPropertiesByPrice(minPrice, maxPrice);
        }

        public static List<Property> properties = new List<Property>();

        public async Task  AddProperty(PropertyDTO propertyDTO)
        {
            var propertyMap=_mapper.Map<Property>(propertyDTO);

            propertyMap.PropertyId = Guid.NewGuid();

            propertyMap.CategoryType =propertyDTO.CategoryType.ToString();

            var result = propertyRepository.Add(propertyMap);
            _unitOfWork.Save();
        }

        public async Task DeleteProperty(Guid id)
        {
            propertyRepository.Remove(id); 
             _unitOfWork.Save();
        }

        public async Task EditProperty(EditPropertyDTO propertyDTO)
        {
            var existingProperty =  propertyRepository.GetById(propertyDTO.Id);
            if (existingProperty == null)
            {
                throw new Exception("Property not found");
            }

            // Map the DTO to the entity
            _mapper.Map(propertyDTO, existingProperty);

            // Save changes to the database
             _unitOfWork.Save();
        }

        public async Task<List<Property>>GetPropertyByCategory(CategoryEnum.CategoryName category)
        {
            return await propertyRepository.GetPropertiesByCategory(category);
        }
    }
}


