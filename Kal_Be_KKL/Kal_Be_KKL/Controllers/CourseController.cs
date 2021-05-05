using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class CourseController : ApiController
    {
        // GET api/<controller>
        public List<Course> Get()
        {
            Course course = new Course();
            List<Course> CourseList = course.Read_Courses();
            return CourseList;
        }

        // GET api/<controller>/5



        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}