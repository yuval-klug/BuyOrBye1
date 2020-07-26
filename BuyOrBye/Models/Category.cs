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
        string category_paired;
        int pair_counter;



        public string Category_name { get => category_name; set => category_name = value; }
        public int NumberOfUse { get => numberOfUse; set => numberOfUse = value; }
        public string Category_paired { get => category_paired; set => category_paired = value; }
        public int Pair_counter { get => pair_counter; set => pair_counter = value; }

        //public Category(string category_name, int numberOfUse, string category_paird, int counter)
        //{
        //    this.category_name = category_name;
        //    this.numberOfUse = numberOfUse;
        //    this.catr
        //}

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

        //update
        public int updateModel(Category category)
        {

            DBservices dbs = new DBservices();
            dbs = dbs.readCategory();
            dbs.dt = CategoryTable(category, dbs.dt);
            dbs.update();
            return 0;
        }

        public DataTable CategoryTable(Category category, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string nameFromDB = (string)(dr["category_name"]);
                string pairedFromDB = (string)(dr["category_paired"]);
                int counter = Convert.ToInt32(dr["pair_counter"]);
                counter +=1;

                if ((nameFromDB == category.Category_name || pairedFromDB== category.Category_name) && (nameFromDB == category.Category_paired || pairedFromDB == category.Category_paired))
                {
                    dr["category_name"] = nameFromDB;
                    dr["category_paired"] = pairedFromDB;
                    dr["pair_counter"] = counter;
                }
            }
            return dt;
        }





    }

}


