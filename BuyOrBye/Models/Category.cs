using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;




namespace BuyOrBye.Models
{
    public class Category
    {
      /*  int id; *///automatic get fill wit sql?
        string category_name;
        int numberOfUse;

        public string Category_name { get => category_name; set => category_name = value; }
        public int NumberOfUse { get => numberOfUse; set => numberOfUse = value; }

        public Category(string category_name, int numberOfUse)
        {
            this.category_name = category_name;
            this.numberOfUse = numberOfUse;
        }

        public Category() { }

        //get//
        public List<Category> getCategoryList()
        {
            DBservices db2 = new DBservices();
            List<Category> listCategoryArr = db2.getCategoryListDAL();
            return listCategoryArr;
        }

        //post//

        public int InsertCategory()
        {
            DBservices db = new DBservices();
            int numAffected = db.InsertCategory(this);
            return numAffected;
        }





    }

}


