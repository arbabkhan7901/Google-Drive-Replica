using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.Entities
{
    public class FileDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int ParentFolderId { get; set; }
        public String FileExt { get; set; }
        public int FileSizeInKB { get; set; }
        public DateTime UploadedOn { get; set; }
        public bool isActive { get; set; }
        public String uniqueName { get; set; }
        public String contentType { get; set; }
    }
}
