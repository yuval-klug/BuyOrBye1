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
        //get users per groupID in order to insert tHe users to user in indecision table
        [HttpGet]
        [Route("api/UserInGroup/GroupNameString/{GroupID}/{IndecisionID}/")]
        public List<UserInGroup> Get(int GroupID, int IndecisionID)
        {
            UserInGroup group = new UserInGroup();
            return group.getUsersInGroup(GroupID, IndecisionID);
        }




    }
}