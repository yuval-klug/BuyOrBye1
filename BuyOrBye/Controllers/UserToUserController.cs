using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class UserToUserController : ApiController
    {
        //get UserToUser
        [HttpGet]
        [Route("api/UserToUser/getWeight/{userSend}/{userReceive}/")]
        public int Get(string userSend, string userReceive)
        {

            UserToUser UserToUserWeight = new UserToUser();

            int returnWeigt = UserToUserWeight.getUserToUserModel(userSend, userReceive);
            return returnWeigt;


        }   //get UserToUser
        [HttpGet]
        [Route("api/UserToUser/getAnswerPerDecision/{userSend}/{indecisionID}/")]
        public float Get(string userSend, int indecisionID)
        {

            UserToUser UserToUserWeight = new UserToUser();

            float returnWeigt = UserToUserWeight.getIndesicionResultModel(userSend, indecisionID);
            return returnWeigt;


        }


        //get string of all the users that took part in this indecision and that their answer was the same as the userSend answer
        [HttpGet]
        [Route("api/UserToUser/{changeWeight}/{UserSend}/{IndecisionID}/{Final_answer}/")]
        public List<UserToUser> UserToUserUpdate(string UserSend, int IndecisionID, bool Final_answer)
        {
            UserToUser userToUser = new UserToUser();
            List<UserToUser> u = new List<UserToUser>();
            u = userToUser.getCorrectAnswerFromModel(UserSend, IndecisionID, Final_answer); //get list of all the user that was participant in this indecision and took the "right" decision
            List<string> currectAnswerPari = new List<string>();

            foreach (var user in u)
            {
                currectAnswerPari.Add(user.UserReceive);

            }

            int updateModel = userToUser.updateUserToUserModel(UserSend, currectAnswerPari); //update the value "numOfCurrectAnswer" in buyOrBye_userToUser table - numOfCurrectAnswer=numOfCurrectAnswer+1


            //get string of all the users that took part in this indecision and that their answer was differnent from the userSend answer
            bool incorrectAnswer = !Final_answer;
            UserToUser userToUserIncorrectAnswer = new UserToUser();
            List<UserToUser> userIncorrectAnswer = new List<UserToUser>();
            userIncorrectAnswer = userToUser.getCorrectAnswerFromModel(UserSend, IndecisionID, incorrectAnswer); //get list of all the user that was participant in this indecision and took the "wrong" decision
            List<string> incorrectAnswerPari = new List<string>();

            foreach (var user in userIncorrectAnswer)
            {
                incorrectAnswerPari.Add(user.UserReceive);

            }
            int updateincorrectAnswerModel = userToUser.updateIncorrectAnswerUserToUserModel(UserSend, incorrectAnswerPari); //update the value "numOfCurrectAnswer" in buyOrBye_userToUser table - numOfCurrectAnswer=numOfCurrectAnswer-1


            return u;

        }

        public void Post([FromBody] UserToUser userToUser)
        {
            userToUser.InsertUserToUser();

        }





    }
}