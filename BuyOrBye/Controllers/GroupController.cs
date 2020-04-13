using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class GroupController : ApiController
    {
        // GET //
        public List<Group> Get()
        {
            Group newGroup = new Group();
            return newGroup.getGroupList();

        }

        //POST//
        public void Post([FromBody]Group group)
        {
            group.InsertGroup();

        }

        //get all the ID that match to the group name
        [HttpGet]
        [Route("api/Group/GroupNameString")]
        public int Get(string UserSend, string Group_name)
        {

            Group createNewGroup = new Group();

            return createNewGroup.getGroupid(UserSend,Group_name);


        }

        public List<Group> Get(string User_manager)
        {
            Group group = new Group();

            return group.getGroupName(User_manager);
        }



    }
}