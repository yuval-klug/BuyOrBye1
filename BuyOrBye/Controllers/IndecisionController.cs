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



    }
}