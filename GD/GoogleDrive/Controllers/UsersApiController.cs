using GD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoogleDrive.Controllers
{
    
    public class UsersApiController : ApiController
    {
        [HttpGet]
        public UsersDTO SaveLogin(String login, String pwd)
        {
            return GD.BAL.UsersBO.ValidateUser(login, pwd);

        }
    }
}
