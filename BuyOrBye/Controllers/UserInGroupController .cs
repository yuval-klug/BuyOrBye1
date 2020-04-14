using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class UserInGroupController : ApiController
    {
        // GET //
        public List<UserInGroup> Get()
        {
            UserInGroup newUserInGroup = new UserInGroup();
            return newUserInGroup.getUserInGroupList();

        }

        //POST//
        public void Post([FromBody]UserInGroup userInGroup)
        {
            userInGroup.InsertUserInGroup();

        }


    }
}