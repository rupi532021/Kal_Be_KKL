using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(string id, string password)
        {
            Employee e = new Employee();
            e = e.LogIn(id, password);
            if (e.Id == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id or password incorrect");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, e);
            }
        }

        [Route("api/Employees/insert_course_of_duty/{Receipt_Course_Date}/{Course_Id}/{Id}")]
        [HttpPost]
        public void insert_course_of_duty(DateTime Receipt_Course_Date, int Course_Id, string Id )
        {
            Employee emp = new Employee();
                emp.insert_course_of_duty(Receipt_Course_Date, Course_Id, Id);
        }
        // POST api/<controller>
        public HttpResponseMessage Post(Employee emp)
        {
            try
            {
                emp.Insert_Employee();
                return Request.CreateResponse(HttpStatusCode.OK, "בוצע");
            }
            catch(SqlException ex) 
            {
                if(ex.Number == 2627)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "תעודת זהות קיימת במערכת");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
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