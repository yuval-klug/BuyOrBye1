using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace BuyOrBye.Models
{
    public class User
    {
        string email;
        string password;
        string first_name;
        string last_name;
        DateTime dateTime;
        string phone;


        //additional properties for indecision table (start)//
       
        int group_id; // foreign key- category table

        string description;
        string img1;
        string img2;
        float percent_option1;
        float percent_option2;
        bool final_answer;

        public string Description { get => description; set => description = value; }
        public string Img1 { get => img1; set => img1 = value; }
        public string Img2 { get => img2; set => img2 = value; }
        public float Percent_option1 { get => percent_option1; set => percent_option1 = value; }
        public float Percent_option2 { get => percent_option2; set => percent_option2 = value; }
        public bool Final_answer { get => final_answer; set => final_answer = value; }
        public int Group_id { get => group_id; set => group_id = value; }




        //additional properties for indecision table (end)//



        private static List<User> UsersList = new List<User>();

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Phone { get => phone; set => phone = value; }
        public static List<User> UsersList1 { get => UsersList; set => UsersList = value; }
      
        public User(string email = null, string password = null, string first_name = null, string last_name = null, DateTime dateTime = default, string phone = null)
        {
            this.Email = email;
            this.Password = password;
            this.First_name = first_name;
            this.Last_name = last_name;
            this.DateTime = dateTime;
            this.Phone = phone;
        }

        public User() { }

          public int InsertUser()
        {
            DBservices db = new DBservices();
            int numAffected = db.InsertUser(this);
            return numAffected;
        }

        public List<User> getUserList()
        {
            DBservices db2 = new DBservices();
            List<User> listUserArr = db2.getUserListDAL();
            return listUserArr;
        }


      //Post Indecision
        public int InsertIndecision()
        {
            DBservices db = new DBservices();
            int numAffected = db.InsertIndecision(this);
            return numAffected;
        }

        //Put User And Category
        public int updateUserAndCategory(User user)
        {

            DBservices dbs = new DBservices();
            dbs = dbs.readUserAndCategory();
            dbs.dt = UserAndCategoryTable(user, dbs.dt);
            dbs.update();
            return 0;
        }


        public DataTable UserAndCategoryTable(User user, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string email = (string)(dr["email"]);
                int categoryID = Convert.ToInt32(dr["category_id"]);
                if (email == user.Email && categoryID == user.Group_id)
                {
                    int numOfuse = Convert.ToInt32(dr["UsingThisCategory"]);
                    numOfuse++;

                    dr["email"] = (string)(user.Email);
                    dr["category_id"] = Convert.ToInt32(user.Group_id);
                    dr["UsingThisCategory"] = numOfuse;
                }
                else
                {
                    int numOfuse = 0;

                    dr["email"] = (string)(user.Email);
                    dr["category_id"] = Convert.ToInt32(user.Group_id);
                    dr["UsingThisCategory"] = numOfuse;
                }
            }
            return dt;
        }


        //get signIn

        public string isSignInSuccModel(string email, string password)
        {

            DBservices dbs = new DBservices();
            return dbs.isSignInSuccBL(email, password);
        }

<<<<<<< HEAD
=======

        //get all the users that exist in the list that the user send
        
        public List<User> createNewGroupModel(string phoneNumbers)
        {

            DBservices dbs = new DBservices();
            return dbs.createNewGroupDB(phoneNumbers);
        }
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
        //update user account
        public int updateUserAccount(User user)
        {

            DBservices dbs = new DBservices();
            dbs = dbs.readUserAccountDAL();
            dbs.dt = updateUserAccountModel(user, dbs.dt);
            dbs.update();
            return 0;
        }
<<<<<<< HEAD

=======
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
        public DataTable updateUserAccountModel(User user, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string thisName = (string)(dr["email"]);
                thisName = thisName.Trim();
                if (thisName == user.Email)
                {
                    if (user.First_name != "")
<<<<<<< HEAD
                        dr["first_name"] = (string)(user.First_name); 
                    if (user.Last_name != "")
                        dr["last_name"] = (string)(user.Last_name); 
                    if (user.Password != "")
                        dr["user_password"] = (string)(user.Password); 
=======
                        dr["first_name"] = (string)(user.First_name);
                    if (user.Last_name != "")
                        dr["last_name"] = (string)(user.Last_name);
                    if (user.Password != "")
                        dr["user_password"] = (string)(user.Password);
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
                    if (user.Phone != "")
                        dr["Phone"] = (string)(user.Phone);

                    break;
                }
            }
            return dt;
        }





<<<<<<< HEAD

=======
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628
    }

}


