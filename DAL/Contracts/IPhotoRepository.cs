using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    internal interface IPhotoRepository : IRepository<Photo, Guid>
    {
        IEnumerable<Photo>propertyPhotos(Guid photoid);

    }
}
