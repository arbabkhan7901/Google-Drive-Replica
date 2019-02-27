using GD.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.DAL
{
    public static class FileDAO
    {
        public static List<FileDTO> GetFileById(int id)
        {
            String query = String.Format(@"Select * from dbo.Files where ParentFolderId = '" + id + "'");
            FileDTO dto = new FileDTO();
            List<FileDTO> list = new List<FileDTO>();
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        while (reader.Read())
                        {
                            dto = FillDTO(reader);
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;

        }

        public static FileDTO getFileByName(String Name)
        {
            String query = String.Format(@"Select * from dbo.Files where uniqueName = '" + Name + "'");
            FileDTO dto = new FileDTO();
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        while (reader.Read())
                        {
                            dto = FillDTO(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dto;
        }

        public static int saveFile(FileDTO dto)
        {
            int count = 0;
            String query = String.Format(@"insert into dbo.Files (Name, ParentFolderId, FileExt, FileSizeInKB, UploadedOn, IsActive, uniqueName, contentType)
                values('{0}',{1},'{2}',{3},'{4}','{5}', '{6}', '{7}')", dto.Name, dto.ParentFolderId, dto.FileExt, dto.FileSizeInKB, dto.UploadedOn, dto.isActive, dto.uniqueName, dto.contentType);
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    count = helper.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return count;
        }
        private static FileDTO FillDTO(SqlDataReader reader)
        {
            var dto = new FileDTO();
            dto.Id = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.ParentFolderId = reader.GetInt32(2);
            dto.FileExt = reader.GetString(3);
            dto.FileSizeInKB = reader.GetInt32(4);
            dto.UploadedOn = reader.GetDateTime(5);
            dto.isActive = reader.GetBoolean(6);
            dto.uniqueName = reader.GetString(7);
            dto.contentType = reader.GetString(8);
            return dto;
        }

        public static List<FileDTO> getFolderMeta(List<FolderDTO> lst)
        {
            List<FileDTO> list = new List<FileDTO>();
            FileDTO dto = new FileDTO();
            try
            {
                for (int i = 0; i < lst.Count(); i++)
                {
                    int parent = lst[i].Id;
                    String query1 = String.Format(@"Select * from dbo.files where ParentFolderId = '" + parent + "'");
                    using (DBHelper helper1 = new DBHelper())
                    {
                        using (var reader1 = helper1.ExecuteReader(query1))
                        {
                            while (reader1.Read())
                            {
                                dto = FillDTO(reader1);
                                list.Add(dto);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }

        public static List<FileDTO> searchFile(String name)
        {
            List<FileDTO> list = new List<FileDTO>();
            String query = String.Format(@"Select * from dbo.Files where name = '"+name+"'");
            FileDTO dto = new FileDTO();
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        while (reader.Read())
                        {
                            dto = FillDTO(reader);
                            list.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public static int deleteFile(int id)
        {
            String query = String.Format(@"delete from dbo.files where Id = '" + id + "'");
            int count = -1;
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    count = helper.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return count;
        }

        public static String[] getFileParentName(List<FileDTO> file)
        {
            String[] arr = new String[file.Count()];
            try
            {
                for (int i = 0; i < file.Count(); i++)
                {
                    String query = String.Format(@"Select name from dbo.Folder where Id = '" + file[i].ParentFolderId + "'");
                    using (DBHelper helper = new DBHelper())
                    {
                        using (var reader = helper.ExecuteReader(query))
                        {
                            while (reader.Read())
                            {
                                arr[i] = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return arr;
        }

        public static List<FileDTO> searchAll (String name)
        {
            List<FileDTO> list = new List<FileDTO>();
            String query = String.Format(@"Select * from dbo.Files");
            FileDTO dto = new FileDTO();
            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        while (reader.Read())
                        {
                            dto = FillDTO(reader);
                            if (dto.Name.Contains(name, comp))
                            {
                                list.Add(dto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;

        }

        public static bool Contains(this String str, String substring,
                              StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException("substring",
                                                "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                            "comp");

            return str.IndexOf(substring, comp) >= 0;
        }
    }
}
