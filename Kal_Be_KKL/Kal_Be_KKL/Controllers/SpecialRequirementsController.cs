using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class SpecialRequirementsController : ApiController
    {
        // GET api/<controller>
        [Route("api/SpecialRequirements/{blockId}/{shiftDate}")]
        [HttpGet]
        public List<BlockShiftRequirementWithName> Get(int blockId,DateTime shiftDate)
        {
            BlockShiftRequirementWithName blockShiftRequirementWithName = new BlockShiftRequirementWithName();
            List<BlockShiftRequirementWithName> SpecialRequirement_List = blockShiftRequirementWithName.Read_SpecialRequirement(blockId, shiftDate);
            return SpecialRequirement_List;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] SpecialRequirement specialRequirement)
        {
            specialRequirement.Insert();
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