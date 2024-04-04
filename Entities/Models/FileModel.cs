using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile file { get; set; }

        public interface IFormFile
        {
            void CopyTo(Stream stream);
        }
    }
}
