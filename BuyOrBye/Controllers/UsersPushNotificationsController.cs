using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class UsersPushNotificationsController : ApiController
    {

        [HttpGet]
        [Route("api/UsersPushNotifications/{email}/{phone}/{token}/")]
        public int Get(string email, string phone, string token)
        {
            UsersNotifications User = new UsersNotifications(email, phone, token);

            return User.checkUser();
        }

        public void Post([FromBody]UsersNotifications u)
        {
            u.InsertUsersNotifications();

        }

        [HttpGet]
        [Route("api/UsersPushNotifications/{emails}/")]
        public List<UsersNotifications> Get(string emails)
        {
            UsersNotifications UsersList = new UsersNotifications();

            return UsersList.getUsers(emails);
        }
    }
}