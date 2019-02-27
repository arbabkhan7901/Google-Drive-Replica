using GD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.BAL
{
    public class FolderBO
    {
        public static List<FolderDTO> getAllFolders()
        {
            return GD.DAL.FolderDAO.getAllFolders();
        }

        public static List<FolderDTO> getFolderById(int id)
        {
            return GD.DAL.FolderDAO.GetFolderById(id);
        }

        public static List<FolderDTO> navFlow (int id)
        {
            return GD.DAL.FolderDAO.navFlow(id);
        }

        public static String getFolderName(int id)
        {
            return GD.DAL.FolderDAO.getFolderName(id);
        }

        public static int getFolderId(String name)
        {
            return GD.DAL.FolderDAO.getFolderId(name);
        }

        public static bool validateFolder(String name)
        {
            return GD.DAL.FolderDAO.ValidateFolder(name);
        }

        public static int deleteFolder(int id)
        {
            return GD.DAL.FolderDAO.deleteFolder(id);
        }

        public static int deleteParentFolder(int id)
        {
            return GD.DAL.FolderDAO.deleteParentFolder(id);
        }

        public static List<FolderDTO> getFolderMeta(int id)
        {
            return GD.DAL.FolderDAO.getFolderMeta(id);
        }

        public static String[] getFolderParentName(List<FolderDTO> folder)
        {
            return GD.DAL.FolderDAO.getFolderParentName(folder);
        }
    }
}
