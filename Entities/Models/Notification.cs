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
        public bool Message { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
