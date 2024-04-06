using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class UserRole
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
