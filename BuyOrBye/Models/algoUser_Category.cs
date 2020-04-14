using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyOrBye.Models
{
    public class algoUser_Category
    {
        string category_name;
        string user_email;


        private static List<algoUser_Category> algoUser_CategoryList = new List<algoUser_Category>();

        public string Category_name { get => category_name; set => category_name = value; }
        public string User_email { get => user_email; set => user_email = value; }

        public algoUser_Category() { }




        public List<algoUser_Category> getalgoUser_CategoryList()
        {
            DBservices db2 = new DBservices();
            List<algoUser_Category> listalgoUser_CategoryArr = db2.getalgoUser_CategoryListDAL();
            return listalgoUser_CategoryArr;
        }

        public int InsertalgoUser_Category()
        {
            DBservices dbsLike = new DBservices();
            int numAffected = dbsLike.algoUser_CategoryDAL(this);
            return numAffected;
        }
    }
}