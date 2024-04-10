using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO.Property
{
    public class PhotoDTO
    {
        [JsonIgnore]
        [DataType(DataType.Upload)]
        public List<IFormFile> ImageFile { get; set; }

        public Guid PropertyId { get; set; }

    }
}