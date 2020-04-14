using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class UserInIndecisionController : ApiController
    {
        public List<UserInIndecision> Get()
        {
            UserInIndecision newUserInIndecision = new UserInIndecision();
            return newUserInIndecision.getUserInIndecisionList();

        }

        public void Post([FromBody]UserInIndecision UserInIndecision)
        {
        
            UserInIndecision.InsertUserInIndecision();

        }


    }
}