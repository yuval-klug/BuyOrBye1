using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;





namespace BuyOrBye.Models
{
    public class UserInGroup
    {
        string user_email;
        int group_groupID;
        float weight;

        public string User_email { get => user_email; set => user_email = value; }
        public int Group_groupID { get => group_groupID; set => group_groupID = value; }
        public float Weight { get => weight; set => weight = value; }

        public UserInGroup(string email, int groupID, float weight)
        {
            this.User_email = email;
            this.Group_groupID = groupID;
            this.Weight = weight;
        }
    


        public UserInGroup() { } 
       

        public List<UserInGroup> getUserInGroupList()
        {
            DBservices db = new DBservices();
            List<UserInGroup> listUserInGroupArr = db.getUserInGroupListDAL();
            return listUserInGroupArr;
        }

        public int InsertUserInGroup()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.InsertUserInGroupDAL(this);
            return numAffected;
        }


        public List<UserInGroup> getUsersByGID(int GroupID)
        {
            DBservices dbs = new DBservices();
            return dbs.getUsersInGroupBL(GroupID);
        }

        //get users in group and insert this to the db
         public List<UserInGroup> getUsersInGroup(int groupID, int IndecisionID)
        {

            DBservices dbs = new DBservices();
            DBservices dbsUIG = new DBservices();
            List<UserInGroup> g = new List<UserInGroup>();

            g = dbs.getUsersInGroupBL(groupID);

            foreach (var i in g)
            {
                string userName = i.User_email;
                int commandReturn = dbsUIG.InsertUII(userName,IndecisionID,groupID);
            }
            return g;

        }

    }

}


