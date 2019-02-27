using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.Entities
{
    public class MixDTO 
    {
        public List<FolderDTO> folder
        {
            get;
            set;
        }
        public List<FileDTO> file
        {
            get;
            set;
        }
        public int fileSize { get; set; }
        public int folderSize { get; set; }
        public int size { get; set; }
    }
}
