using Entities.Models;
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
        Property AddProperty(Property property);
        void DeleteProperty(Guid id);
    }
}