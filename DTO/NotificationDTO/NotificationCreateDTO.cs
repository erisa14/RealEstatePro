using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.NotificationDTO
{
    public class NotificationCreateDTO
    {
        public Guid UserIdReceiver { get; set; }
        public Guid UserIdSender { get; set; }
        public string Message { get; set; } = null!;
    }
}
