using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Property
    {
        public Property()
        {
            Photos = new HashSet<Photo>();
            Reservations = new HashSet<Reservation>();
            Users = new HashSet<User>();
        }

        public Guid PropertyId { get; set; }
        public string CategoryType { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public int SquareArea { get; set; }
        public int NumberOfFloors { get; set; }
        public string Description { get; set; } = null!;
        public int Status { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
