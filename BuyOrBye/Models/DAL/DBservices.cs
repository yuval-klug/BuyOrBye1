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






    // Zion functions //

    public List<UsersNotifications> getUsersToPush(string emails)
    {
        List<UsersNotifications> UsersNotificationsList = new List<UsersNotifications>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_UsersPushNotifications u where u.email in(" + emails + ")";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row

                UsersNotifications u = new UsersNotifications();

                u.Email = (string)dr["email"];
                u.Phone = (string)dr["phone"]; //te type is string beacauze te nuber start wit 0
                u.Token = (string)dr["token"];

                UsersNotificationsList.Add(u);
            }

            return UsersNotificationsList;
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





    public int InsertUsersNotifications(UsersNotifications user)
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

        String cStr;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}')", user.Email, user.Phone, user.Token);
        String prefix1 = "INSERT INTO buyOrBye_UsersPushNotifications (email, phone, token)";
        cStr = prefix1 + sb1.ToString();

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



    public int checkForUsersPush(UsersNotifications user)
    {
        SqlConnection con = null;
        int count = 0;
        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_UsersPushNotifications where email='" + user.Email + "' AND phone='" + user.Phone + "'" + " AND token = '" + user.Token + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                count++;
            }

            return count;
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
        sb1.AppendFormat("Values('{0}', '{1}')", category.Category_name, category.NumberOfUse);
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
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}')", user.Email, user.Password, user.First_name, user.Last_name, dateResult, user.Phone);
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
    public int InsertUserInGroup(UserInGroup userInGroup)
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

        String cStr = BuildInsertCommandUserInGroup(userInGroup);      // helper method to build the insert string

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
    private String BuildInsertCommandUserInGroup(UserInGroup userInGroup)
    {
        String command1;

        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        int num = 0;
        sb1.AppendFormat("Values('{0}', '{1}','{2}')", userInGroup.User_email, userInGroup.Group_groupID,num);
        String prefix1 = "INSERT INTO buyOrBye_userInGroup (user_manager,group_groupID,weight_)";
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
        
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}','{6}','{7}')", user.Email, user.Group_id, user.Description, user.Img1, user.Img2, user.Percent_option1, user.Percent_option2, user.Final_answer);
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


    //sign in get
    public User isSignInSuccBL(string email, string password)
    {
        List<User> UserList = new List<User>();
        SqlConnection con = null;
        User u = new User();

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_user where email='" + email + "' AND user_password='" + password + "'";
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

            if (u.Email == email)
                return u;
            else
                return null;
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
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}')", userInGroup.User_email, userInGroup.Group_groupID, userInGroup.Weight);
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
                indecision.Close_indecision = Convert.ToInt32(dr["close_indecision"]);




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
        int num = 0;
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}','{6}','{7}')", indecision.Group_groupID, indecision.User_email, indecision.Description_, indecision.Img1, indecision.Img2, indecision.Final_answer, indecision.CurrectAnswerPercent,num);
        String prefix1 = "INSERT INTO buyOrBye_Indecision (group_groupID, user_email, description_, img1, img2, final_answer, currectAnswerPercent,closeIndecision)";
        command1 = prefix1 + sb1.ToString();
        return command1;
    }
    //sign in
    public int getUserToUserWeightBL(string userSend, string userReceive)
    {
        List<UserToUser> UserToUserList = new List<UserToUser>();
        SqlConnection con = null;
        //   UserToUser u = new UserToUser();
        int numOfCurrectAnswer = 0;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select numOfCurrectAnswer from buyOrBye_UserToUser where user_emailSend='" + userSend + "' AND user_emailreceive='" + userReceive + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                numOfCurrectAnswer = Convert.ToInt32(dr["numOfCurrectAnswer"]);

                //u.Email = (string)dr["email"];
                //u.Password = (string)dr["user_password"];
                //u.First_name = (string)dr["first_name"];
                //u.Last_name = (string)dr["last_name"];
                //u.DateTime = (DateTime)dr["created_date"]; //add te date of tis day
                //u.Phone = (string)dr["phone"]; //te type is string beacauze te nuber start wit 0

                //UserList.Add(u);
            }

            return numOfCurrectAnswer;

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
    //get user to user

    public List<UserToUser> getIndesicionResultDAL(string userSend, int indecisionID)
    {
        List<UserToUser> UserToUserList = new List<UserToUser>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_User_In_Indecision uii inner join buyOrBye_UserToUser utu on uii.indecisionID='"+ indecisionID + "' where (utu.user_emailSend = '"+ userSend + "' and utu.user_emailreceive= uii.userInGroup_emailParticipant) and uii.indecisionID = '"+ indecisionID + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                UserToUser u = new UserToUser();


                u.UserReceive = (string)dr["userInGroup_emailParticipant"];
                u.Final_answer = (bool)Convert.ToBoolean(dr["pic_num"]);
                u.Percent_option1 = (float)Convert.ToDouble(dr["percent_option1"]);
                u.Percent_option2 = (float)Convert.ToDouble(dr["percent_option2"]);
                u.UserSend = (string)dr["user_emailSend"];
                u.NumOfCurrectAnswer = Convert.ToInt32(dr["numOfCurrectAnswer"]);

                UserToUserList.Add(u);
            }


            return UserToUserList;
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

    //post final answer and percent to indecision
    //post//

    public int updateIndecisionBL(int final_answer, float percent_, int indecisionID)
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

        String cStr = BuildInsertCommandUserToUser(final_answer, percent_, indecisionID);      // helper method to build the insert string

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
    private String BuildInsertCommandUserToUser(int final_answer, float percent_, int indecisonID)
    {
        String command1;
        String prefix1 = "UPDATE buyOrBye_Indecision SET currectAnswerPercent = '" + percent_ + "', final_answer = '" + final_answer + "' WHERE indecisionID = '" + indecisonID + "'";
        command1 = prefix1;
        return command1;
    }

    //get correct users from db
    public List<UserToUser> getCorrectUsersFromBD(int IndecisionID, bool Final_answer)
    {
        List<UserToUser> UserToUserList = new List<UserToUser>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select uii.userInGroup_emailParticipant from buyOrBye_User_In_Indecision uii where uii.indecisionID='" + IndecisionID + "' and uii.pic_num='" + Final_answer + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                UserToUser c = new UserToUser();

                c.UserReceive = (string)dr["userInGroup_emailParticipant"];
                UserToUserList.Add(c);
            }

            return UserToUserList;
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

    //update userToUserTable
    public DBservices updateTableUserToUser(string userSend, DataTable dt)
    {
        SqlConnection con = null;
        try
        {
            con = connect("flightsDBConnectionString");
            da = new SqlDataAdapter("select * from buyOrBye_UserToUser utu where utu.user_emailSend='" + userSend + "'", con);
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

    public DBservices readUserToUser(string userSend)
    {
        SqlConnection con = null;
        try
        {
            con = connect("flightsDBConnectionString");
            da = new SqlDataAdapter("select * from buyOrBye_UserToUser utu where utu.user_emailSend='" + userSend + "'", con);
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


    // get users that find in the phone string
    public List<User> createNewGroupDB(string phoneNumbers)
    {
        List<User> UserList = new List<User>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_User u where u.phone in(" + phoneNumbers + ")";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row

                User u = new User();

                u.Email = (string)dr["email"];
                u.Password = (string)dr["user_password"];
                u.First_name = (string)dr["first_name"];
                u.Last_name = (string)dr["last_name"];
                u.DateTime = (DateTime)dr["created_date"]; //add te date of tis day
                u.Phone = (string)dr["phone"]; //te type is string beacauze te nuber start wit 0

                UserList.Add(u);
            }

            return UserList;
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

    //get group id that match the groupname
    public int getGroupDB(string UserSend, string groupName)
    {
        SqlConnection con = null;
        int Group_ID = 0;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_Group WHERE group_name ='" + groupName + "' and user_manager='" + UserSend + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                Group_ID = Convert.ToInt32(dr["groupID"]);

            }

            return Group_ID;
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

    //get all group names for the groups list

    public List<Group> getGroupNameDB(string nameG)
    {
        List<Group> GroupList = new List<Group>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_Group g where g.user_manager='" + nameG + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Group g = new Group();

                g.Group_name = (string)dr["group_name"];
                g.Group_ID = Convert.ToInt32(dr["groupID"]);
                g.User_manager = (string)(dr["user_manager"]);
                GroupList.Add(g);
            }

            return GroupList;
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


    public List<Indecision> getIndecisionByName(string userSend)
    {
        List<Indecision> IndecisionList = new List<Indecision>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_Indecision i where i.user_email='" + userSend + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Indecision ind = new Indecision();

                ind.User_email = (string)dr["user_email"];
                ind.IndecisionID = Convert.ToInt32(dr["indecisionID"]);
                ind.Group_groupID = Convert.ToInt32(dr["group_groupID"]);
                ind.Description_ = (string)(dr["description_"]);
                ind.CurrectAnswerPercent = (float)Convert.ToDouble(dr["currectAnswerPercent"]);
                ind.Img1 = (string)(dr["img1"]);
                ind.Img2 = (string)(dr["img2"]);
                ind.Final_answer = Convert.ToBoolean(dr["final_answer"]);
                ind.Close_indecision = Convert.ToInt32(dr["closeIndecision"]);

                IndecisionList.Add(ind);
            }

            return IndecisionList;
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


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// USER IN DECISION DAL START //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <param name="indecision"></param>
    /// <returns></returns>
    /// 

    //POST
    public int UserInIndecisionDAL(UserInIndecision UserInIndecision)
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

        String cStr = BuildInsertCommandIndecision(UserInIndecision);      // helper method to build the insert string

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

    //GET

    public List<UserInIndecision> getUserInIndecisionListDAL()
    {
        List<UserInIndecision> newListIndecision = new List<UserInIndecision>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_Indecision";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                UserInIndecision indecision = new UserInIndecision();
                indecision.IndecisionID = Convert.ToInt32(dr["indecisionID"]);
                indecision.UserInGroup_groupID = Convert.ToInt32(dr["userInGroup_groupID"]);
                indecision.UserInGroup_emailParticipant = (string)dr["userInGroup_emailParticipant"];
                indecision.Percent_option1 = (float)Convert.ToDouble(dr["percent_option1"]);
                indecision.Percent_option2 = (float)Convert.ToDouble(dr["percent_option2"]);
                indecision.Pic_num = (bool)dr["pic_num"];

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

    private String BuildInsertCommandIndecision(UserInIndecision uid)
    {
        //te email neef to be excits in te uer table! we need to tink wo to take te email from te current user
        String command1;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        // (indecisionID, userInGroup_emailParticipant, userInGroup_groupID, pic_num, percent_option1, percent_option2)
        int reactionTime = 0;
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}','{6}')", uid.IndecisionID, uid.UserInGroup_emailParticipant, uid.UserInGroup_groupID, uid.Pic_num, uid.Percent_option1, uid.Percent_option2,reactionTime);
        String prefix1 = "insert into buyOrBye_User_In_Indecision (indecisionID,userInGroup_emailParticipant,userInGroup_groupID,pic_num,percent_option1,percent_option2,reactionTime)";
        command1 = prefix1 + sb1.ToString();
        return command1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// USER IN DECISION DAL END //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// Create Category DAL START //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public List<algoCategory> getalgoCategoryListDAL()
    {
        List<algoCategory> newListalgoCategory = new List<algoCategory>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_algo_category";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                algoCategory algoCategory = new algoCategory();
                algoCategory.Category_name = (string)dr["category_name"];
                algoCategory.Key_1 = (string)dr["key_1"];
                algoCategory.Key_2 = (string)dr["key_2"];
                algoCategory.Key_3 = (string)dr["key_3"];

                newListalgoCategory.Add(algoCategory);
            }

            return newListalgoCategory;
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

    public int algoCategoryDAL(algoCategory algoCategory)
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

        String cStr = BuildInsertCommandalgoCategory(algoCategory);      // helper method to build the insert string

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
    private String BuildInsertCommandalgoCategory(algoCategory algoCategory)
    {
        //te email neef to be excits in te uer table! we need to tink wo to take te email from te current user
        String command1;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}')", algoCategory.Category_name, algoCategory.Key_1, algoCategory.Key_2, algoCategory.Key_3);
        String prefix1 = "INSERT INTO buyOrBye_algo_category (category_name, key_1, key_2, key_3)";
        command1 = prefix1 + sb1.ToString();
        return command1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// Create Category DAL END //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// User_Category DAL START //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public List<algoUser_Category> getalgoUser_CategoryListDAL()
    {
        List<algoUser_Category> newListalgoUser_Category = new List<algoUser_Category>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_algo_user_category";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {   // Read till the end of the data into a row
                algoUser_Category algoUser_Category = new algoUser_Category();
                algoUser_Category.Category_name = (string)dr["category_name"];
                algoUser_Category.User_email = (string)dr["user_email"];

                newListalgoUser_Category.Add(algoUser_Category);
            }

            return newListalgoUser_Category;
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

    public int algoUser_CategoryDAL(algoUser_Category algoUser_Category)
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

        String cStr = BuildInsertCommandalgoUser_Category(algoUser_Category);      // helper method to build the insert string

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
    private String BuildInsertCommandalgoUser_Category(algoUser_Category algoUser_Category)
    {
        //te email neef to be excits in te uer table! we need to tink wo to take te email from te current user
        String command1;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' )", algoUser_Category.Category_name, algoUser_Category.User_email);
        String prefix1 = "INSERT INTO buyOrBye_algo_user_category (category_name, user_email)";
        command1 = prefix1 + sb1.ToString();
        return command1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// User_Category DAL END //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// Similarity PAred DAL START //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public List<algoSimilarity_paired> getalgoSimilarity_pairedListDAL()
    {
        List<algoSimilarity_paired> newListalgoSimilarity_paired = new List<algoSimilarity_paired>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "SELECT * FROM buyOrBye_algo_paired_similarity";
            SqlCommand cmd = new SqlCommand(getThisPLS, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            while (dr.Read())
            {   // Read till the end of the data into a row
                algoSimilarity_paired algoSimilarity_paired = new algoSimilarity_paired();
                algoSimilarity_paired.Category_name = (string)dr["category_name"];
                algoSimilarity_paired.Category_paired = (string)dr["category_paired"];
                //algoSimilarity_paired.Pair_weight = (float)Convert.ToDouble(dr["pair_weight"]); //Fix if wight will be neccesary
                algoSimilarity_paired.Pair_counter = Convert.ToInt32(dr["pair_counter"]);

                newListalgoSimilarity_paired.Add(algoSimilarity_paired);
            }

            return newListalgoSimilarity_paired;
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

    public int algoSimilarity_pairedDAL(algoSimilarity_paired algoSimilarity_paired)
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

        String cStr = BuildInsertCommandalgoSimilarity_paired(algoSimilarity_paired);      // helper method to build the insert string

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
    private String BuildInsertCommandalgoSimilarity_paired(algoSimilarity_paired algoSimilarity_paired)
    {
        //te email neef to be excits in te uer table! we need to tink wo to take te email from te current user
        String command1;
        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}')", algoSimilarity_paired.Category_name, algoSimilarity_paired.Category_paired, algoSimilarity_paired.Pair_weight, algoSimilarity_paired.Pair_counter);
        String prefix1 = "INSERT INTO buyOrBye_algo_paired_similarity (category_name, category_paired, pair_weight, pair_counter)";
        command1 = prefix1 + sb1.ToString();
        return command1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////// Similarity Paired DAL END //////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///


    //get indecision in order to stop indecision
    public List<Indecision> getIndecisionByIDBL(int indecisionID) {


        List<Indecision> newListIndecision = new List<Indecision>();

        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create the connection
            string getThisPLS = "select * from buyOrBye_Indecision i where i.indecisionID='" + indecisionID + "'";
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
                indecision.Close_indecision = Convert.ToInt32(dr["closeIndecision"]);





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

    //update Close indecision == 1 - the user choose his final answer

    public int ReadAndPutIndecisionModel(int indecisionID)
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

        String cStr = BuildInsertCommandIndecision(indecisionID);      // helper method to build the insert string

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
    private String BuildInsertCommandIndecision(int indecisonID)
    {
        String command1;
        String prefix1 = "UPDATE buyOrBye_Indecision SET closeIndecision = '1' WHERE indecisionID = '" + indecisonID + "'";
        command1 = prefix1;
        return command1;
    }


    //get indecisions of friends



    public List<Indecision> getFriendIndecisionByUserPartBL(string userName, int closeIndecision)
    {
        List<Indecision> IndecisionList = new List<Indecision>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_Indecision i left join buyOrBye_User_In_Indecision uii on i.indecisionID = uii.indecisionID WHERE i.closeIndecision = '" + closeIndecision + "' and uii.userInGroup_emailParticipant='" + userName + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Indecision i = new Indecision();

                i.IndecisionID = Convert.ToInt32(dr["indecisionID"]);
                i.Img1 = (string)dr["img1"];
                i.Img2 = (string)(dr["img2"]);
                i.Description_ = (string)(dr["description_"]);
                i.User_email = (string)(dr["user_email"]);
                i.Group_groupID = Convert.ToInt32(dr["userInGroup_groupID"]);
                i.Percent_option1 = (float)Convert.ToDouble(dr["percent_option1"]);
                i.Percent_option2 = (float)Convert.ToDouble(dr["percent_option2"]);
                IndecisionList.Add(i);
            }

            return IndecisionList;
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

    //updte te user received answer in user in indecision table

    public DBservices readUserInIndecisionBL()
    {
        SqlConnection con = null;
        try
        {
            con = connect("flightsDBConnectionString");
            da = new SqlDataAdapter("select * from buyOrBye_User_In_Indecision", con);
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


    //post User To User after create new group
    public int InsertUserToUserBL(UserToUser userToUser)
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

        String cStr = BuildInsertCommandUserToUser(userToUser);      // helper method to build the insert string

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
    private String BuildInsertCommandUserToUser(UserToUser userToUser)
    {
        String command1;

        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        int num = 0;
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}')", userToUser.UserSend, userToUser.UserReceive, userToUser.NumOfCurrectAnswer,num);
        String prefix1 = "INSERT INTO buyOrBye_UserToUser (user_emailSend, user_emailreceive, numOfCurrectAnswer,numOfGroup)";
        command1 = prefix1 + sb1.ToString();
        return command1;

    }

    public List<User> giveUserEmailBL(string nameC)
    {
        List<User> userList = new List<User>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_User where email='"+nameC+"'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                User u = new User();

                u.First_name = (string)dr["first_name"];
                u.Last_name = (string)dr["last_name"];
                userList.Add(u);
            }

            return userList;
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

    //get indecision id

    
    public List<Indecision> getIndecisionIDfromBL(string name)
    {
        List<Indecision> IndecisionList = new List<Indecision>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT top(1) * FROM buyOrBye_Indecision where user_email = '"+name+"' ORDER BY indecisionID DESC";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Indecision c = new Indecision();

                c.IndecisionID = Convert.ToInt32(dr["indecisionID"]);
                IndecisionList.Add(c);
            }

            return IndecisionList;
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

    

   public List<UserInGroup> getUsersInGroupBL(int groupID)
    {
        List<UserInGroup> countryUserInGroupList = new List<UserInGroup>();
        SqlConnection con = null;

        try
        {
            con = connect("flightsDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from buyOrBye_userInGroup where group_groupID='"+groupID+"'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                UserInGroup c = new UserInGroup();

                c.User_email = (string)dr["user_email"];
                countryUserInGroupList.Add(c);
            }

            return countryUserInGroupList;
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

    public int InsertUII(string userName, int IndecisionID, int GroupID)
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

        String cStr = insertUserInIndecision(userName, IndecisionID, GroupID);      // helper method to build the insert string

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
    private string insertUserInIndecision(string userName, int IndecisionID, int GroupID)
    {
        String command1;

        StringBuilder sb1 = new StringBuilder();
        // use a string builder to create the dynamic string
        int num = 0;
        sb1.AppendFormat("Values('{0}', '{1}' ,'{2}','{3}','{4}','{5}',{6})", IndecisionID, userName, GroupID, num, num,num,num);
        String prefix1 = "INSERT INTO buyOrBye_User_In_Indecision (indecisionID,userInGroup_emailParticipant,userInGroup_groupID,percent_option1, percent_option2,reactionTime,pic_num)";
        command1 = prefix1 + sb1.ToString();
        return command1;

    }

    public DBservices readCategory()
    {
        SqlConnection con = null;
        try
        {
            con = connect("flightsDBConnectionString");
            da = new SqlDataAdapter("select * from buyOrBye_algo_paired_similarity", con);
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




























