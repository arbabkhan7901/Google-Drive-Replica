using GD.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.DAL
{
    public static class FolderDAO
    {
        public static List<FolderDTO> getAllFolders()
        {
            List<FolderDTO> list = new List<FolderDTO>();
            int count = 0;
            String query = String.Format("Select * from dbo.folder where ParentFolderId = '"+count+"'");
            FolderDTO dto = new FolderDTO();
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        while (reader.Read())
                        {
                            dto = FillDTO(reader);
                            if (dto != null)
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

        public static bool ValidateFolder(String name)
        {
            var query = String.Format(@"Select * from dbo.folder Where name = '" + name + "'");
            FolderDTO dto = new FolderDTO();
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        if (reader.Read())
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        private static FolderDTO FillDTO(SqlDataReader reader)
        {
            var dto = new FolderDTO();
            dto.Id = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.ParentFolderId = reader.GetInt32(2);
            dto.CreatedOn = reader.GetDateTime(3);
            dto.isActive = reader.GetBoolean(4);
            return dto;
        }

        public static List<FolderDTO> GetFolderById(int id)
        {
            String query = String.Format(@"Select * from dbo.Folder where ParentFolderId = '"+id+"'");
            List<FolderDTO> list = new List<FolderDTO>();
            FolderDTO dto = new FolderDTO();
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

        public static String getFolderName(int id)
        {
            String query = String.Format(@"Select * from dbo.Folder where Id = '" + id + "'");
            FolderDTO dto = new FolderDTO();
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
            return dto.Name;
        }

        public static int getFolderId(String name)
        {
            String query = String.Format(@"Select * from dbo.Folder where name = '" + name + "'");
            FolderDTO dto = new FolderDTO();
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
            return dto.Id;
        }

        public static List<FolderDTO> navFlow(int id)
        {
            List<FolderDTO> list = new List<FolderDTO>();
            bool flag = true;
            try
            {
                while (flag)
                {
                    String query = String.Format(@"Select * from dbo.Folder where id = '"+id+"'");
                    using (DBHelper helper = new DBHelper())
                    {
                        using (var reader = helper.ExecuteReader(query))
                        {
                            while (reader.Read())
                            {
                                int num = reader.GetInt32(2);
                                id = num;
                                if(num == 0)
                                {
                                    FolderDTO dto1 = new FolderDTO();
                                    dto1.Id = 0;
                                    dto1.Name = "Home";
                                    dto1.ParentFolderId = 0;
                                    dto1.isActive = true;
                                    dto1.CreatedOn = DateTime.Now;
                                    list.Add(dto1);
                                    flag = false;
                                    break;
                                }
                                String query1 = String.Format(@"Select * from dbo.Folder where id = '"+num+"'");
                               using (DBHelper helper1 = new DBHelper())
                                {
                                    using(var reader2 = helper1.ExecuteReader(query1))
                                    {
                                        while (reader2.Read())
                                        {
                                            FolderDTO dto = FillDTO(reader2);
                                            list.Add(dto);
                                        }
                                    }
                                }
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

        public static int deleteFolder(int id)
        { 
            String query = String.Format(@"delete from dbo.folder where Id = '" + id + "'");
            int count = -1;
            try
            {
                using(DBHelper helper = new DBHelper())
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

        public static int deleteParentFolder(int id)
        {
            String query = String.Format(@"delete from dbo.folder where ParentFolderId = '" + id + "'");
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

        public static List<FolderDTO> getFolderMeta(int id)
        {
            List<FolderDTO> list = new List<FolderDTO>();
            FolderDTO dto = new FolderDTO();
            int parent = 0;
            String query = String.Format(@"Select * from dbo.folder where Id = '"+id+"'");
            String query2 = String.Format(@"Select * from dbo.Folder");
            try
            {
                using(DBHelper helper = new DBHelper())
                {
                    if(id > 0)
                    {
                        using (var reader = helper.ExecuteReader(query))
                        {
                            if (reader.Read())
                            {
                                dto = FillDTO(reader);
                                list.Add(dto);
                            }
                        }
                    }
                    else
                    {
                        using (var reader2 = helper.ExecuteReader(query2))
                        {
                            if (reader2.Read())
                            {
                                dto = FillDTO(reader2);
                                list.Add(dto);
                            }
                        }
                    }
                }
                if (dto != null)
                {
                    for(int i = 0; i < list.Count(); i++)
                    {
                        parent = list[i].Id;
                        String query1 = String.Format(@"Select * from dbo.folder where ParentFolderId = '" + parent + "'");
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
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }

        public static String[] getFolderParentName(List<FolderDTO> folder)
        {
            String[] arr = new String[folder.Count()];
            try
            {
                for (int i = 0; i < folder.Count(); i++)
                {
                    if(folder[i].ParentFolderId == 0)
                    {
                        arr[i] = "Root";
                        i++;
                    }
                    String query = String.Format(@"Select name from dbo.Folder where Id = '"+folder[i].ParentFolderId+"'");
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
           catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }     
            return arr;
        }
    }
}

