using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid UserIdReceiver { get; set; }
        public Guid UserIdSender { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Message { get; set; } = null!;
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
