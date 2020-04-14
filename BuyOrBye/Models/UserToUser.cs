using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;




namespace BuyOrBye.Models
{
    public class UserToUser
    {
        string userSend;
        string userReceive;
        int indecisionID;

        //userInIndecision

        bool final_answer;
        float percent_option1;
        float percent_option2;
        int numOfCurrectAnswer;



        

        static List<UserToUser> UserToUserList = new List<UserToUser>();



        public UserToUser() { }

        public string UserSend { get => userSend; set => userSend = value; }
        public string UserReceive { get => userReceive; set => userReceive = value; }
        public static List<UserToUser> UserToUserList1 { get => UserToUserList; set => UserToUserList = value; }
        public bool Final_answer { get => final_answer; set => final_answer = value; }
        public float Percent_option1 { get => percent_option1; set => percent_option1 = value; }
        public float Percent_option2 { get => percent_option2; set => percent_option2 = value; }
        public int IndecisionID { get => indecisionID; set => indecisionID = value; }
        public int NumOfCurrectAnswer { get => numOfCurrectAnswer; set => numOfCurrectAnswer = value; }

        public int getUserToUserModel(string userSend, string userReceive)
        {

            DBservices dbs = new DBservices();
            int numberOfCurrectAnswers= dbs.getUserToUserWeightBL(userSend, userReceive);

            return 0;
        }

        public float getIndesicionResultModel(string userSend, int indecisionID)
        {
            DBservices db2 = new DBservices();
            List<UserToUser> listUserArr = db2.getIndesicionResultDAL(userSend,indecisionID);
            float pic0_rank = 0;
            float pic1_rank = 0;
            float mehane = 0;
            float finalePic1Precentage = 0;

            foreach (var row in listUserArr) // Sum connection mehane
            {
                mehane += row.numOfCurrectAnswer;
            }

            foreach (var row in listUserArr)
            {
                pic0_rank += ((row.numOfCurrectAnswer / mehane) * row.percent_option1);
                pic1_rank += ((row.numOfCurrectAnswer / mehane) * row.percent_option2);
            }

            finalePic1Precentage = (pic1_rank / (pic0_rank + pic1_rank)); // this value should be return for pic1 %
            if(finalePic1Precentage >=0.5) //pic1 has been choosen
            {
                int the_final_answer = 1;
                DBservices dbUpdate = new DBservices();
                int finalAnswer = dbUpdate.updateIndecisionBL(the_final_answer, finalePic1Precentage, indecisionID);

            }
            else
            {
                int the_final_answer = 0;
                DBservices dbUpdate = new DBservices();
                int finalAnswer = dbUpdate.updateIndecisionBL(the_final_answer, finalePic1Precentage, indecisionID);

            }
            if (finalePic1Precentage >= 0.5) 
                finalePic1Precentage = 1; //pic1 has been choosen
            else
                finalePic1Precentage = 0; //pic2 has been choosen

           return finalePic1Precentage;
        }

        public List<UserToUser> getCorrectAnswerFromModel(string UserSend, int IndecisionID, bool Final_answer)
        {
            string u = UserSend;
            DBservices dbs = new DBservices();
            return dbs.getCorrectUsersFromBD(IndecisionID, Final_answer);
        }

        public int updateUserToUserModel(string UserSend, List<string> userToUserList) 
        {
           
                DBservices dbs = new DBservices();
                dbs = dbs.readUserToUser(UserSend);
                dbs.dt = updateTableUserToUser(userToUserList, dbs.dt); //update the counter in UserToUser table = numOfCurrectAnswer = numOfCurrectAnswer+1
                dbs.update();
                return 0;
        } //get the UserToUser table from the DB in order to update this table with counter ++


        public DataTable updateTableUserToUser(List<string> userToUserList, DataTable dt) //update the counter in UserToUser table = numOfCurrectAnswer = numOfCurrectAnswer+1
        {
            foreach (DataRow dr in dt.Rows)
            {
                int counter = Convert.ToInt32(dr["numOfCurrectAnswer"]);
                string userParti = (string)(dr["user_emailreceive"]);

                //להוס'ף פה אם הרשומה לא ק''מת ב'וזר טו 'וזר להוס'ף

                if (counter < 10 && userToUserList.Contains(userParti))
                {
                    counter++; ;
                    dr["numOfCurrectAnswer"] = Convert.ToString(counter);
                }
                //else if (counter > 0 && userToUserList.Contains(userParti))
                //{
                //    counter = counter - 1; ;
                //    dr["numOfCurrentAnswer"] = counter;
                //} //when user choose the other option- the condition in not correct only the inside of the loop
            }
            return dt;
        }


        public int updateIncorrectAnswerUserToUserModel(string UserSend, List<string> userToUserList)
        {

            DBservices dbs = new DBservices();
            dbs = dbs.readUserToUser(UserSend);
            dbs.dt = updateIncorrectAnswerTableUserToUser(userToUserList, dbs.dt); //update the counter in UserToUser table = numOfCurrectAnswer = numOfCurrectAnswer+1
            dbs.update();
            return 0;
        } //get the UserToUser table from the DB in order to update this table with counter --

        public DataTable updateIncorrectAnswerTableUserToUser(List<string> userToUserList, DataTable dt) //update the counter in UserToUser table = numOfCurrectAnswer = numOfCurrectAnswer-1
        {
            foreach (DataRow dr in dt.Rows)
            {
                int counter = Convert.ToInt32(dr["numOfCurrectAnswer"]); //get this values from the DB
                string userParti = (string)(dr["user_emailreceive"]); // get this values from the DB


                if (counter > 0 && userToUserList.Contains(userParti)) //check if this user answer on this decition and took "wrong" answer
                {
                    counter = counter - 1;
                    dr["numOfCurrectAnswer"] = Convert.ToString(counter);
                }
                
            }
            return dt;
        }
    }

}


