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
        [Route("api/Courses_Of_Dutys/Delete_Course_Of_Duty/")]
        [HttpPost]
        public HttpResponseMessage Delete_Course_Of_Duty(Courses_Of_Duty cod)
        {
            try
            {
                if (cod.Delete_Course_Of_Duty())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "קורס הוסר בהצלחה");//{fResult: 'True', message: 'קורס הוסר בהצלחה'}
                }
                else 
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "קורס לא הוסר עקב תקלה");//{fResult: 'True', message: 'קורס הוסר בהצלחה'}
                }
            }
            catch(Exception ex) 
            {
                return Request.CreateResponse(HttpStatusCode.OK, "{fResult: 'False', message: 'Carta shgiya'}", System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json")); //Request.CreateErrorResponse(HttpStatusCode.OK, ex.Message);
            }
        }

    }
}