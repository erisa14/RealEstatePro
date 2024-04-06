using DAL.Contracts;
using DTO.Property;
using Entities.Models;
using Helpers.Enumerations;
using Microsoft.EntityFrameworkCore;
using StructureMap;
using System;
using System.Linq;

namespace DAL.Concrete
{
    internal class PropertyRepository : BaseRepository<Property, Guid>, IPropertyRepository
    {

        public PropertyRepository(RealEstateContext dbContext) : base(dbContext)
        {
        }

      

        public Property GetById(Guid id)
        {
            var property = context.FirstOrDefault(a => a.PropertyId == id);
            return property;
        }


        public async Task<List<Property>> GetPropertiesByCategory(CategoryEnum.CategoryName categoryName)
        {
            string categoryString = categoryName.ToString();

            var properties = await context.Where(p => p.CategoryType == categoryString)  
                .ToListAsync();

            return properties;
        }

        public async Task<List<Property>> GetPropertiesByPrice(decimal minPrice, decimal maxPrice)
        {
            var properties= await context.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
            return properties;
        }
    }
}
