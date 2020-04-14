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

<<<<<<< HEAD
        //put User And Category --not necessary??
=======
        //put User And Category
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
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

<<<<<<< HEAD
=======
        //get all the users that exist in the list that the user send
        [HttpGet]
        [Route("api/User/phoneNumbersString")]
        public List<User> Get(string phoneNumber)
        {

            User createNewGroup = new User();

            return createNewGroup.createNewGroupModel(phoneNumber);


        }

>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
        [HttpPut]
        [Route("api/User/{User}")]
        public int updateUserAccount(User user)
        {
            User userAccount = new User();
            return userAccount.updateUserAccount(user);
        }


<<<<<<< HEAD





=======
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
    }
}