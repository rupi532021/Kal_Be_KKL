using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class Courses_Of_DutysController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/Courses_Of_Dutys/GetAllCoursesOfDuty/{id}")]
        [HttpGet]
        public List<Courses_Of_Duty> GetAllCoursesOfDuty(string id)
        {
            Courses_Of_Duty c = new Courses_Of_Duty();
            List<Courses_Of_Duty> CoursesOfDuty = c.GetAllCoursesOfDuty(id);
            return CoursesOfDuty;
        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

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