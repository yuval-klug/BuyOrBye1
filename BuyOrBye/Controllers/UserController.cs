using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class UserController : ApiController
    {
        // GET //
        public List<User> Get()
        {
            User newUser = new User();
            return newUser.getUserList();

        }

        //POST//
        public void Post([FromBody]User user)
        {
            user.InsertUser();

        }

        [HttpPost]
        [Route("api/User/Indecision")]
        public void PostIndecision([FromBody]User user)
        {
            user.InsertIndecision();

        }

        //put User And Category --not necessary??
        [HttpPut]
        [Route("api/User/UserAndCategory")]
        public int CountryUp(User user)
        {
            User userAndCategory = new User();
            return userAndCategory.updateUserAndCategory(user);
        }

        //get signIn
        [HttpGet]
        [Route("api/User/signIn")]
        public string Get(string email, string password)
        {
          
            User signInUser = new User();

          
            return signInUser.isSignInSuccModel(email, password);
          

        }

        [HttpPut]
        [Route("api/User/{User}")]
        public int updateUserAccount(User user)
        {
            User userAccount = new User();
            return userAccount.updateUserAccount(user);
        }







    }
}