using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Property
{
    public class PhotoDTO
    {
        public Guid PhotoId { get; set; }

        public byte[]? Photos { get; set; }
    }
}
