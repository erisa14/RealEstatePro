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
    internal class PhotoRepository : BaseRepository<Photo, Guid>, IPhotoRepository
    {
        public PhotoRepository(RealEstateContext dbContext) : base(dbContext)
        {
        }

        public int CountByPropertyId(Guid propertyId)
        {
            var count= context.Where(p=>p.PropertyId == propertyId).Count();
            return count;
        }
    }
}
