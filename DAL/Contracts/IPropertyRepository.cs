using DTO.Property;
using Entities.Models;
using Helpers.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPropertyRepository : IRepository<Property, Guid>
    {
        Property GetById(Guid id);
        Task <List<Property>> GetPropertiesByCategory(CategoryEnum.CategoryName categoryName);

        Task<List<Property>> GetPropertiesByPrice(decimal minPrice, decimal maxPrice);
   }
}