using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyOrBye.Models
{
    public class algoCategory
    {
        string category_name;
        string key_1;
        string key_2;
        string key_3;

        private static List<algoCategory> algoCategoryList = new List<algoCategory>();

        public algoCategory(){ }

        public string Category_name { get => category_name; set => category_name = value; }
        public string Key_1 { get => key_1; set => key_1 = value; }
        public string Key_2 { get => key_2; set => key_2 = value; }
        public string Key_3 { get => key_3; set => key_3 = value; }


        public List<algoCategory> getalgoCategoryList()
        {
            DBservices db2 = new DBservices();
            List<algoCategory> listalgoCategoryArr = db2.getalgoCategoryListDAL();
            return listalgoCategoryArr;
        }

        public int InsertalgoCategory()
        {
            DBservices dbsLike = new DBservices();
            int numAffected = dbsLike.algoCategoryDAL(this);
            return numAffected;
        }
    }
}