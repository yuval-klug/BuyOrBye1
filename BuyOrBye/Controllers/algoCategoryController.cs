using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class algoCategoryController : ApiController
    {
        public List<algoCategory> Get()
        {
            algoCategory newalgoCategory = new algoCategory();
            return newalgoCategory.getalgoCategoryList();

        }

        public void Post([FromBody]algoCategory algoCategory)
        {
            algoCategory.InsertalgoCategory();

        }

    }
}
