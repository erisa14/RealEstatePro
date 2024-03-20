using DTO.PropertyDTO;
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
        PropertyDTO AddProperty(PropertyDTO property);


    }
}