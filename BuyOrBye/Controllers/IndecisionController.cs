using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class IndecisionController : ApiController
    {
        public List<Indecision> Get()
        {
            Indecision newIndecision = new Indecision();
            return newIndecision.getIndecisionList();

        }

        public void Post([FromBody]Indecision indecision)
        {
            indecision.InsertIndecision();

        }
        [HttpGet]
        [Route("api/Indecision/getIndecisionByUserEmail/{userSend}/")]
        public List<Indecision> Get(string userSend)
        {
            Indecision inde = new Indecision();

            return inde.ReadIndecision(userSend);
        } 
        
        //get indecision to stop indecision
        [HttpGet]
        [Route("api/Indecision/getIndecisionByID/{IndecisionID}/")]
        public List<Indecision> Get(int IndecisionID)
        {
            Indecision inde = new Indecision();

            return inde.getIndecisionByID(IndecisionID);
        }


        //update Close indecision == 1 - the user choose his final answer
        [HttpPut]
        [Route("api/Indecision/PutIndecision")]
        public int PutIndecision(Indecision IndecisionID)
        {
            Indecision indecision = new Indecision();
            return indecision.PutIndecisionModel(IndecisionID.IndecisionID);
        }

        //get indecisions of friends
        [HttpGet]
        [Route("api/Indecision/getFriendIndecisionByUserPart/{UserPart}/{openIndeicsion}/")]
        public List<Indecision> Get(string UserPart, int openIndeicsion)
        {
            Indecision inde = new Indecision();

            return inde.getFriendIndecisionByUserPartModel(UserPart, openIndeicsion);
        }

        // get indecision id
        
        [Route("api/Indecision/{User_manager}/{Bool}/")]
        public List<Indecision> Get(string User_manager, bool Bool)
        {
            Indecision cc = new Indecision();

            return cc.ReadIndecisionID(User_manager);
        }




    }
}