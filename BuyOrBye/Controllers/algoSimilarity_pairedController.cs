using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class algoSimilarity_pairedController : ApiController
    {
        public List<algoSimilarity_paired> Get()
        {
            algoSimilarity_paired newalgoSimilarity_paired = new algoSimilarity_paired();
            return newalgoSimilarity_paired.getalgoSimilarity_pairedList();

        }

        public void Post([FromBody]algoSimilarity_paired algoSimilarity_paired)
        {
            algoSimilarity_paired.InsertalgoSimilarity_paired();

        }
    }
}
