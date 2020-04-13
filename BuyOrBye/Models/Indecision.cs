using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace BuyOrBye.Models
{
    public class Indecision
    {
        int indecisionID;
        int group_groupID;
        string user_email;
        string description_;
        string img1;
        string img2;
        bool final_answer;
        float currectAnswerPercent;


        private static List<Indecision> UsersList = new List<Indecision>();

        public int IndecisionID { get => indecisionID; set => indecisionID = value; }
        public int Group_groupID { get => group_groupID; set => group_groupID = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public string Description_ { get => description_; set => description_ = value; }
        public string Img1 { get => img1; set => img1 = value; }
        public string Img2 { get => img2; set => img2 = value; }
        public bool Final_answer { get => final_answer; set => final_answer = value; }
        public float CurrectAnswerPercent { get => currectAnswerPercent; set => currectAnswerPercent = value; }

        public Indecision() { }


        public List<Indecision> getIndecisionList()
        {
            DBservices db2 = new DBservices();
            List<Indecision> listIndecisionArr = db2.getIndecisionListDAL();
            return listIndecisionArr;
        }

        public int InsertIndecision()
        {
            DBservices dbsLike = new DBservices();
            int numAffected = dbsLike.IndecisionDAL(this);
            return numAffected;
        }

        public List<Indecision> ReadIndecision(string name)
        {

            DBservices dbs = new DBservices();
            return dbs.getIndecisionByName(name);
        }




    }

}


