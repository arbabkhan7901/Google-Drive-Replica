using GD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.BAL
{
    public static class FileBO
    {
        public static List<FileDTO> getFileById(int id)
        {
            return GD.DAL.FileDAO.GetFileById(id);
        }

        public static FileDTO getFileByName(String name)
        {
            return GD.DAL.FileDAO.getFileByName(name);
        }

        public static int saveFile(FileDTO dto)
        {
            return GD.DAL.FileDAO.saveFile(dto);
        }

        public static int deleteFile(int id)
        {
            return GD.DAL.FileDAO.deleteFile(id);
        }

        public static List<FileDTO> getFileMeta(List<FolderDTO> folder)
        {
            return GD.DAL.FileDAO.getFolderMeta(folder);
        }

        public static String[] getFileParentName(List<FileDTO> file)
        {
            return GD.DAL.FileDAO.getFileParentName(file);
        }

        public static List<FileDTO> serachAll(String name)
        {
            return GD.DAL.FileDAO.searchAll(name);
        }
    }
}
