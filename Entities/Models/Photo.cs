using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Photo
    {
        public Guid Id { get; set; }
        public string Path { get; set; } = null!;
        public byte[] Data { get; set; } = null!;
        public Guid PropertyId { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}
