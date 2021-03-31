using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class Day_In_ShiftController : ApiController
    {
        // GET api/<controller>
        public List<Day_In_Shift> Get()
        {
            Day_In_Shift dayinshift = new Day_In_Shift();
            List<Day_In_Shift> DayInShiftList = dayinshift.ReadDayInShift();
            return DayInShiftList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post(Day_In_Shift dayinshift)
        {
            dayinshift.InsertDayInShift();
        }
        [Route("api/Day_In_Shift/SmartPost/{areaId}")]
        [HttpPost]
        public void SmartPost(int areaId)
        {
            //צריך לעבור על כל הגושים בינתיים נעבור על אחד
            //צריך לעבור על כל התאריכים בחודש הבא בינתיים רק על אחד
            Day_In_Shift shift = new Day_In_Shift();
            DateTime date = DateTime.Today.AddMonths(2); //לשנות ל1
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            Area area = new Area();
            var blockIds = area.getBlockIds(areaId);
            for (var day = firstDayOfMonth.Date; day.Date <= lastDayOfMonth.Date; day = day.AddDays(1))
            {
                shift.Shift_Date = day;
                foreach (int blockId in blockIds)
                {
                    List<RequirementForSpecificShift> permantReqs = shift.GetPermantReq(blockId);
                    List<RequirementForSpecificShift> speciaelReqs = shift.GetSpeciaelReq(blockId, shift.Shift_Date);
                    AssignToShift(permantReqs, shift, areaId, blockId);
                    AssignToShift(speciaelReqs, shift, areaId, blockId);
                }
            }
        }

        private void AssignToShift(List<RequirementForSpecificShift> reqs, Day_In_Shift shift,int areaId,int blockId)
        {
            foreach (var req in reqs)
            {
                for (int i = 0; i < req.Quantity; i++)
                {
                    Employee matchEmployee = shift.FindMatchEmployee(areaId,shift.Shift_Date,req.Requirement_Id);
                    if (matchEmployee.Id != null)
                        shift.InsertEmployeeToShift(matchEmployee.Id, blockId,shift.Shift_Date,req.Requirement_Id);
                    else
                        break;
                }
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}