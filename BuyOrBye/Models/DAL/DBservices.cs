using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using BuyOrBye.Models;


public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //This method creates a connection to the database according to the connectionString name in the web.config //
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }




    //CATEGORY//

//Get Category (start)//
    public List<Category> getCategoryListDAL()
    {
        List<Category> newListCategory = new List<Category>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_Category";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                Category category = new Category();
                category.Category_name = (string)dr["category_name"];
                category.NumberOfUse = Convert.ToInt32(dr["numberOfUse"]);



                newListCategory.Add(category);
            }

            return newListCategory;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

//Get Category (end)//
    public void update()
        {
            da.Update(dt);
        }

    
//POST Category (start)//

    public int InsertCategory(Category category)
{

    SqlConnection con;
    SqlCommand cmd;

    try
    {
        con = connect("flightsDBConnectionString"); // create the connection
    }
    catch (Exception ex)
    {
        // write to log
        throw (ex);
    }

    String cStr = BuildInsertCommandCategory(category);      // helper method to build the insert string

    cmd = CreateCommand(cStr, con);             // create the command

    try
    {
        int numEffected = cmd.ExecuteNonQuery(); // execute the command
        return numEffected;
    }
    catch (Exception ex)
    {
        return 0;
        // write to log
        throw (ex);
    }

    finally
    {
        if (con != null)
        {
            // close the db connection
            con.Close();
        }
    }

}
    private String BuildInsertCommandCategory(Category category)
{
    String command1;

    StringBuilder sb1 = new StringBuilder();
    // use a string builder to create the dynamic string
    sb1.AppendFormat("Values('{0}', '{1}')", category.Category_name , category.NumberOfUse);
    String prefix1 = "INSERT INTO buyOrBye_Category (category_name, numberOfUse)";
    command1 = prefix1 + sb1.ToString();
    return command1;

}

    //POST Category (end)//

    //GET User (start)//

    public List<User> getUserListDAL()
    {
        List<User> newListCountry = new List<User>();

        SqlConnection con = null;

        
        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_User";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           
            
            while (dr.Read())
            {   // Read till the end of the data into a row
                User user = new User();
                user.Email = (string)dr["email"];
                user.Password = (string)dr["user_password"];
                user.First_name = (string)dr["first_name"];
                user.Last_name = (string)dr["last_name"];
                user.DateTime = (DateTime)dr["created_date"]; //add te date of tis day
                user.Phone = (string)dr["phone"]; //te type is string beacauze te nuber start wit 0

                newListCountry.Add(user);
            }
            return newListCountry;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //GET User (end)//

    //POST User (start)//
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandUser(user);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private String BuildInsertCommandUser(User user)
    {
        String command1;
        DateTime todayDateVal = DateTime.Today; // need to find te rigt function
        string year = todayDateVal.Year.ToString();
        string month = todayDateVal.Month.ToString();
        string day = todayDateVal.Day.ToString();
        string dateResult = year + "-" + month + "-" + day;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}')", user.Email, user.Password, user.First_name, user.Last_name, dateResult ,user.Phone);
        String prefix1 = "INSERT INTO buyOrBye_User (email, user_password, first_name, last_name, created_date, phone)";
        command1 = prefix1 + sb1.ToString();
        return command1;

    }
    //POST User (end)//

    //GRT Group (start)//
    public List<Group> getGroupListDAL()
    {
        List<Group> newListGroup = new List<Group>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_Group";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                Group group = new Group();
                group.User_manager = (string)dr["user_manager"];
                group.Group_ID = (int)dr["groupID"];




                newListGroup.Add(group);
            }

            return newListGroup;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //GET Group (end)//
    //POST Group (start)//
    public int InsertGroup(Group group)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandGroup(group);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private String BuildInsertCommandGroup(Group group)
    {
        String command1;

        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}')", group.User_manager, group.Group_name);
        String prefix1 = "INSERT INTO buyOrBye_Group (user_manager,group_name)";
        command1 = prefix1 + sb1.ToString();
        return command1;

    }


    //POST Group (start)//
    public int InsertUserInGroup(Group group)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandUserInGroup(group);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private String BuildInsertCommandUserInGroup(Group group)
    {
        String command1;

        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}')", group.User_manager, group.Group_ID);
        String prefix1 = "INSERT INTO buyOrBye_userInGroup (user_manager,group_groupID)";
        command1 = prefix1 + sb1.ToString();
        return command1;

    }

    //POST Indecision (start)//
    public int InsertIndecision(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandIndecision(user);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private String BuildInsertCommandIndecision(User user)
    {
        //te email neef to be excits in te uer table! we need to tink wo to take te email from te current user
        String command1;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}','{6}','{7}')", user.Email, user.Group_id, user.Description, user.Img1,user.Img2, user.Percent_option1, user.Percent_option2,user.Final_answer);
        String prefix1 = "INSERT INTO buyOrBye_Indecision (email, group_groupID, description_ind, img_option1, img_option2, percent_option1, percent_option2, final_answer)";
        command1 = prefix1 + sb1.ToString();
        return command1;

    }
    //POST Indecision (end)//

    //put User and Category (from user model)
    public DBservices readUserAndCategory()
    {
        SqlConnection con = null;
        try
        {
            con = connect("flightsDBConnectionString");
            da = new SqlDataAdapter("select * from buyOrBye_User_and_Category", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }

        catch (Exception ex)
        {
            // write errors to log file
            // try to handle the error
            throw ex;
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }


        return this;

    }


    //sign in
    public string isSignInSuccBL(string email, string password)
    {
        List<User> UserList = new List<User>();
        SqlConnection con = null;
        User u = new User();

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_user where email='" + email + "' AND user_password='"+ password + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
               

                u.Email = (string)dr["email"];
                u.Password = (string)dr["user_password"];
                u.First_name = (string)dr["first_name"];
                u.Last_name = (string)dr["last_name"];
                u.DateTime = (DateTime)dr["created_date"]; //add te date of tis day
                u.Phone = (string)dr["phone"]; //te type is string beacauze te nuber start wit 0

                UserList.Add(u);
            }

            if (u.Email== email)
                return "hey " + u.First_name + " welcome back!"  ;
            else
                return "oops! wrong password or userName,please try again";
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }
    //user in group

        //get//
    public List<UserInGroup> getUserInGroupListDAL()
    {
        List<UserInGroup> newListUserInGroup = new List<UserInGroup>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_userInGroup";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                UserInGroup userInGroup = new UserInGroup();
                userInGroup.User_email = (string)dr["user_email"];
                userInGroup.Group_groupID = Convert.ToInt32(dr["Group_groupID"]);
                userInGroup.Weight = (float)Convert.ToDouble(dr["weight_"]);

                

                newListUserInGroup.Add(userInGroup);
            }

            return newListUserInGroup;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //post//

    public int InsertUserInGroupDAL(UserInGroup userInGroup)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("flightsDBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommandUserInGroupDAL(userInGroup);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommandUserInGroupDAL(UserInGroup userInGroup)
        {
            String command1;

            StringBuilder sb1 = new StringBuilder();
            // use a string builder to create the dynamic string
            sb1.AppendFormat("Values('{0}', '{1}' ,'{2}')",userInGroup.User_email, userInGroup.Group_groupID,userInGroup.Weight);
            String prefix1 = "INSERT INTO buyOrBye_userInGroup (user_email, group_groupID, weight_)";
            command1 = prefix1 + sb1.ToString();
            return command1;

        }
    //indecision
    //get//

    

        public List<Indecision> getIndecisionListDAL()
    {
        List<Indecision> newListIndecision = new List<Indecision>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_Indecision";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                Indecision indecision = new Indecision();
                indecision.IndecisionID = Convert.ToInt32(dr["indecisionID"]);
                indecision.Group_groupID = Convert.ToInt32(dr["group_groupID"]);
                indecision.User_email = (string)dr["user_email"];
                indecision.Description_ = (string)dr["description_"];
                indecision.Img1 = (string)dr["img1"];
                indecision.Img2 = (string)dr["img2"];
                indecision.Final_answer = (bool)dr["final_answer"];



                newListIndecision.Add(indecision);
            }

            return newListIndecision;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //post//

    public int IndecisionDAL(Indecision indecision)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandIndecision(indecision);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private String BuildInsertCommandIndecision(Indecision indecision)
    {
        //te email neef to be excits in te uer table! we need to tink wo to take te email from te current user
        String command1;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}','{6}','{7}')", indecision.Group_groupID,indecision.User_email, indecision.Description_, indecision.Img1, indecision.Img2,indecision.Final_answer, indecision.Percent_option1, indecision.Percent_option2);
        String prefix1 = "INSERT INTO buyOrBye_Indecision ( group_groupID, user_email, description_, img1, img2, final_answer, percent_option1, percent_option2)";
        command1 = prefix1 + sb1.ToString();
        return command1;
    }

    //update user account
    public DBservices readUserAccountDAL()
    {
        SqlConnection con = null;
        try
        {
            con = connect("flightsDBConnectionString");
            da = new SqlDataAdapter("select * from buyOrBye_User", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }

        catch (Exception ex)
        {
            // write errors to log file
            // try to handle the error
            throw ex;
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }


        return this;

    }


}

























