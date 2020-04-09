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


    }
}