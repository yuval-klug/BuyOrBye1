using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace BuyOrBye.Models
{
    public class UserInIndecision
    { 

        int indecisionID;
        string userInGroup_emailParticipant;
        int userInGroup_groupID;
        bool pic_num;
        float percent_option1;
        float percent_option2;

        private static List<UserInIndecision> UsersList = new List<UserInIndecision>();

        public int IndecisionID { get => indecisionID; set => indecisionID = value; }
        public string UserInGroup_emailParticipant { get => userInGroup_emailParticipant; set => userInGroup_emailParticipant = value; }
        public int UserInGroup_groupID { get => userInGroup_groupID; set => userInGroup_groupID = value; }
        public bool Pic_num { get => pic_num; set => pic_num = value; }
        public float Percent_option1 { get => percent_option1; set => percent_option1 = value; }
        public float Percent_option2 { get => percent_option2; set => percent_option2 = value; }

        public UserInIndecision() { }


        public List<UserInIndecision> getUserInIndecisionList()
        {
            DBservices db2 = new DBservices();
            List<UserInIndecision> listUserInIndecisionArr = db2.getUserInIndecisionListDAL();
            return listUserInIndecisionArr;
        }

        public int InsertUserInIndecision()
        {
            DBservices dbsLike = new DBservices();
            int numAffected = dbsLike.UserInIndecisionDAL(this);
            return numAffected;
        }



    }

}


