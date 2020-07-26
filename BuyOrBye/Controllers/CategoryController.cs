using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuyOrBye.Models;

namespace BuyOrBye.Controllers
{
    public class CategoryController : ApiController
    {
        // GET //
        public List<Category> Get()
        {
            Category newCategory = new Category();
            return newCategory.getCategoryList();

        }

        //POST//
        public void Post([FromBody]Category category)
        {
            category.InsertCategory();

        }

        //put update category[0],category[1]
        [HttpPut]
        [Route("api/Category/Update/{Category}")]
        public int Put(Category category)
        {

            Category c = new Category();


            return c.updateModel(category);


        }


    }
}