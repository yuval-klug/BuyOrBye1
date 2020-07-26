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

        [HttpPut]
        [Route("api/UserInIndecision/updateAnswer")]
        public int CountryUp(UserInIndecision userInIndecision)
        {
            UserInIndecision userInIndecision1 = new UserInIndecision();
            return userInIndecision1.updateUserInIndecisionModel(userInIndecision);
        }



    }
}