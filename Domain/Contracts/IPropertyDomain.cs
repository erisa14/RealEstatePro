using DTO.Property;
using Entities.Models;
using Helpers.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPropertyDomain
    {
        IList<PropertyDTO> GetAllProperties();
        PropertyDTO GetPropertyById(Guid id);

        Task<List<Property>> GetPropertyByCategory(CategoryEnum.CategoryName category);
        Task<List<Property>> GetAllPropertiesByPrice(decimal minPrice, decimal maxPrice);
        Task  AddProperty(PropertyDTO propertyDTO);
        // IList<PropertyDTO> EditProperty(PropertyDTO propertyDTO);
        Task DeleteProperty(Guid id);

        Task EditProperty(EditPropertyDTO propertyDTO);


    }
}