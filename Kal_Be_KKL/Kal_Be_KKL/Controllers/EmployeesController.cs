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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "שם משתמש או סיסמא לא נכונים");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, e);
            }
        }


        [Route("api/Employees/GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            Employee e = new Employee();
            List<Employee> emps = e.GetAllEmployee();
                return Request.CreateResponse(HttpStatusCode.OK, emps);
            
        }
     

        [Route("api/Employees/insert_course_of_duty/{Receipt_Course_Date}/{Course_Id}/{Id}")]
        [HttpPost]
        public void insert_course_of_duty(DateTime Receipt_Course_Date, int Course_Id, string Id)
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
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "תעודת זהות קיימת במערכת");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
        }
        [Route("api/Employees/PutEmployee/")]
        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage PutEmployee([FromBody] Employee emp)
        {
            try
            {

                emp.Edit_Employee();
                return Request.CreateResponse(HttpStatusCode.OK, "בוצע");
            }



            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
        }
        public HttpResponseMessage getAreaOrRegionAndJob(string id)
        {
            Employee e = new Employee();
            Worker_In_Area wia = e.getAreaAndJob(id);
            if (wia.Area_Id != 0)
                return Request.CreateResponse(HttpStatusCode.OK, wia);
            Worker_In_Region wir = e.getRegionAndJob(id);
            if (wir.Region_Id != 0)
                return Request.CreateResponse(HttpStatusCode.OK, wir);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "תקלה בשרת");
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}