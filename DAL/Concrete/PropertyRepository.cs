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
            // Convert the enum to its string representation
            string categoryString = categoryName.ToString();

            // Query the database to get properties by the category string
            var properties = await context.Where(p => p.CategoryType == categoryString) // Correctly reference the CategoryType property
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
