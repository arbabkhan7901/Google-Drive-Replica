using GD.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace GoogleDrive.Controllers
{
   
    public class FolderController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public int CreateFolder(String name, int id)
        {
            bool flag = GD.BAL.FolderBO.validateFolder(name);
            if (flag)
                return GD.BAL.UsersBO.CreateFolder(name, id);
            else
                return -1;        
        }

        [HttpGet]
        public int deleteFolder(int id)
        {
            GD.BAL.FolderBO.deleteParentFolder(id);
            int count = GD.BAL.FolderBO.deleteFolder(id);
            return count;
        }

        [HttpGet]
        public String getFolderName(int id)
        {
            String name = GD.BAL.FolderBO.getFolderName(id);
            return name;
        }

        [HttpGet]
        public int getFolderId(String name)
        {
            name = Regex.Replace(name, @"\s+", "");
            int id = GD.BAL.FolderBO.getFolderId(name);
            return id;
        }
    }
}