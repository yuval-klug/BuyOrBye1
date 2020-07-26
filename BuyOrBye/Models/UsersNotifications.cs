using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyOrBye.Models
{
    public class UsersNotifications
    {
        string email;
        string phone;
        string token;

        public UsersNotifications(string email, string phone, string token)
        {
            this.email = email;
            this.phone = phone;
            this.token = token;
        }

        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Token { get => token; set => token = value; }

        public UsersNotifications() { }

        public List<UsersNotifications> getUsers(string emails)
        {
            DBservices db2 = new DBservices();
            List<UsersNotifications> listUsers = db2.getUsersToPush(emails);
            return listUsers;
        }

        public int InsertUsersNotifications()
        {
            DBservices db = new DBservices();
            int numAffected = db.InsertUsersNotifications(this);
            return numAffected;
        }

        public int checkUser()
        {
            DBservices db = new DBservices();
            return db.checkForUsersPush(this);
        }

    }
}