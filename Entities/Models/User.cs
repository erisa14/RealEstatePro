using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class User
    {
        public User()
        {
            Notifications = new HashSet<Notification>();
            Properties = new HashSet<Property>();
            Roles = new HashSet<Role>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public Guid RoleId { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
