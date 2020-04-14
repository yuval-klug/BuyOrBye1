using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class algoUser_CategoryController : ApiController
    {
        public List<algoUser_Category> Get()
        {
            algoUser_Category newalgoUser_Category = new algoUser_Category();
            return newalgoUser_Category.getalgoUser_CategoryList();

        }

        public void Post([FromBody]algoUser_Category algoUser_Category)
        {
            algoUser_Category.InsertalgoUser_Category();

        }

    }
}