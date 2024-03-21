using DTO.PropertyDTO;
using Entities.Models;
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
        IList<PropertyDTO> GetAllPropertiesByCategory();
        IList<PropertyDTO> GetAllPropertiesByPrice(decimal minPrice, decimal maxPrice);

        List<Property> AddProperty(PropertyDTO propertyDTO);

    }
}