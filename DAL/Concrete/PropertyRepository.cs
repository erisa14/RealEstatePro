using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class PropertyRepository : BaseRepository<Property, Guid>, IPropertyRepository
    {

        public PropertyRepository(RealEstateContext dbContext) : base(dbContext)
        {

        }
        public Property GetById(Guid id)
        {
            var property = context.Where(a => a.PropertyId == id).FirstOrDefault();
            return property;
        }



        void IPropertyRepository.DeleteProperty(Guid id)
        {
            var propertyToDelete = context.FirstOrDefault(p => p.PropertyId == id);
            if (propertyToDelete != null)
            {
                context.Remove(propertyToDelete);
            }
            else
            {
                throw new ArgumentException($"Property with ID {id} not found");
            }
        }

    }
}
