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

        // POST api/<controller>
        public void Post([FromBody] SpecialRequirement specialRequirement)
        {
            int Current_Value = specialRequirement.If_SpecialRequirement_Is_Exist(specialRequirement);
            if (Current_Value == 0)
                specialRequirement.Insert();
            else
            {
                specialRequirement.Quantity += Current_Value;
                specialRequirement.Update_SpecialRequirement();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [Route("api/SpecialRequirements/Delete/{blockId}/{shiftDate}/{Requirement_Id}")]
        [HttpDelete]
        public List<BlockShiftRequirementWithName> Delete(int blockId, DateTime shiftDate, int Requirement_Id)
        {
            BlockShiftRequirementWithName blockShiftRequirementWithName = new BlockShiftRequirementWithName();
            blockShiftRequirementWithName.Delete_SpecialRequirement(blockId, shiftDate, Requirement_Id);
            return Get(blockId, shiftDate);
        }
    }
}