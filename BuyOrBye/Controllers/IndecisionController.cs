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
        [Route("api/Indecision/getIndecisionByUserEmail")]
        public List<Indecision> Get(string userSend)
        {
            Indecision inde = new Indecision();

            return inde.ReadIndecision(userSend);
        }
        


    }
}