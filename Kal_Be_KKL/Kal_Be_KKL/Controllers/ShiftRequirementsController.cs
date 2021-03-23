using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class ShiftRequirementsController : ApiController
    {
        // GET api/<controller>
        public List<ShiftRequirement> Get()
        {
            ShiftRequirement shiftRequirement = new ShiftRequirement();
            List<ShiftRequirement> shiftRequirementList = shiftRequirement.Read_Requirements();
            return shiftRequirementList;
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