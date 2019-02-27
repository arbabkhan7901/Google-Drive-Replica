using GD.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.DAL
{
    public static class UsersDAO
    {
        public static UsersDTO ValidateUser(String pLogin, String pPassword)
        {
            var query = String.Format("Select * from dbo.Users Where Login='{0}' and Password='{1}'", pLogin, pPassword);
            UsersDTO dto = new UsersDTO();
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    using (var reader = helper.ExecuteReader(query))
                    {
                        if (reader.Read())
                        {
                            dto = FillDTO(reader);
                        }
                        else
                        {
                            dto.Id = -1;
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

        public static int CreateFolder(String name, int id)
        {
            var query = String.Format(@"insert into dbo.Folder(Name, ParentFolderId, Createdon, IsActive) Values('{0}', {1}, '{2}', '{3}')", name, id, DateTime.Now, 1);
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    return helper.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        private static UsersDTO FillDTO(SqlDataReader reader)
        {
            var dto = new UsersDTO();
            dto.Id = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.Login = reader.GetString(2);
            dto.Password = reader.GetString(3);
            dto.Email = reader.GetString(4);
            return dto;
        }
    }
}
