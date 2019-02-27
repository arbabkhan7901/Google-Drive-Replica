using GD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.BAL
{
    public class UsersBO
    {
        public static UsersDTO ValidateUser(String pLogin, String pPassword)
        {
            return GD.DAL.UsersDAO.ValidateUser(pLogin, pPassword);
        }

        public static int CreateFolder(String name, int id)
        {
            return GD.DAL.UsersDAO.CreateFolder(name, id);
        }
    }
}
