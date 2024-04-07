using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Reservation
    {
        public Guid ReservationId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Status { get; set; } = null!;
        public Guid? PropertyId { get; set; }

        public virtual Property? Property { get; set; }
    }
}
