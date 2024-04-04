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
        public IEnumerable<Photo>propertyPhotos(Guid photoid)
        {
            var propertyPhotos = context.Where(x => x.PhotoId == photoid).ToList();
            return propertyPhotos;
        }
    }
}
