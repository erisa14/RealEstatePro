using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Property
    {
        public Property()
        {
            Reservations = new HashSet<Reservation>();
            Users = new HashSet<User>();
        }

        public Guid PropertyId { get; set; }
        public string CategoryType { get; set; }
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public int SquareArea { get; set; }
        public int NumberOfFloors { get; set; }
        public string Description { get; set; } = null!;
        public int Status { get; set; }
        public Guid? PhotoId { get; set; }

        public virtual Photo? Photo { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
