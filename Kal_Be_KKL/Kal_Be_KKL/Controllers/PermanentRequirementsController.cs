using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class PermanentRequirementsController : ApiController
    {
        // GET api/<controller>
        [Route("api/PermanentRequirements/{blockId}")]
        [HttpGet]
        public List<BlockShiftRequirementWithName> Get(int blockId)
        {
            BlockShiftRequirementWithName permanentRequirement = new BlockShiftRequirementWithName();
            List<BlockShiftRequirementWithName> PermanentRequirementsList = permanentRequirement.Read_PermanentRequirement(blockId);
            return PermanentRequirementsList;
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