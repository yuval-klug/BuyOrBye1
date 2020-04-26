using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;





namespace BuyOrBye.Models
{
    public class Group
    {
        string user_manager;
        string group_name;
        int group_ID;

        public string User_manager { get => user_manager; set => user_manager = value; }
        public string Group_name { get => group_name; set => group_name = value; }
        public int Group_ID { get => group_ID; set => group_ID = value; }

        public Group(string user_manager, string group_name,int groupID)
        {
            this.User_manager = user_manager;
            this.Group_name = group_name;
            this.Group_ID = groupID;
        }

        public Group() { }

        public List<Group> getGroupList()
        {
            DBservices db = new DBservices();
            List<Group> listGroupArr = db.getGroupListDAL();
            return listGroupArr;
        }

        public int InsertGroup()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.InsertGroup(this);
            return numAffected;
        }

        //get the group id that match the group name
        
        public int getGroupid(string UserSend,string group_Name)
        {

            DBservices dbs = new DBservices();
            //create array of users in group and insert this to the db
            int groupID = dbs.getGroupDB(UserSend, group_Name);
            return groupID;
        }


        //get all group names for the groups list
        public List<Group> getGroupName(string name)
        {

            DBservices dbs = new DBservices();
            return dbs.getGroupNameDB(name);
        }

   


    }

}


