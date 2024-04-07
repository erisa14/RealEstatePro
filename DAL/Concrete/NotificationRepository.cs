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
    internal class NotificationRepository : BaseRepository<Notification, Guid>, INotificiationRepository
    {
        public NotificationRepository(RealEstateContext dbContext) : base(dbContext)
        {
        }
    }
}
