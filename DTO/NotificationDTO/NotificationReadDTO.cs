using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.NotificationDTO
{
    public class NotificationReadDTO
    {
        public Guid NotificationId { get; set; }
        public Guid UserIdReceiver { get; set; }
        public Guid UserIdSender { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Message { get; set; } = null!;
    }
}
