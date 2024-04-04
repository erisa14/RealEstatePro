using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Properties = new HashSet<Property>();
        }


        public Guid PhotoId { get; set; }
        public byte[] Photos { get; set; } = null!;


        public virtual ICollection<Property> Properties { get; set; }
    }
}
