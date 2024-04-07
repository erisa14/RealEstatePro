using DTO.NotificationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface INotificationDomain
    {
        public List<NotificationReadDTO> GetAll();
        public NotificationReadDTO Get(Guid id);
        public Guid CreateNotification(NotificationCreateDTO dto);
        public void DeleteNotification(Guid id);
        public List<NotificationReadDTO> GetNotificationsBySender(Guid sender);
        public List<NotificationReadDTO> GetNotificationsByReceiver(Guid receiver);
    }
}
