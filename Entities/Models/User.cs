using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class User
    {
        public User()
        {
            Notifications = new HashSet<Notification>();
            UserRoles = new HashSet<UserRole>();
            Properties = new HashSet<Property>();
        }

        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Password { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
