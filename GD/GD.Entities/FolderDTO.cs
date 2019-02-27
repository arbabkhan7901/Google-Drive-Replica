﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.Entities
{
    public class FolderDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int ParentFolderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool isActive { get; set; }
    }
}
