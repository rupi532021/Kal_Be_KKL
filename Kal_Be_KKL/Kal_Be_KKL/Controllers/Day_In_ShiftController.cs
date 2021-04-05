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
        [Route("api/Day_In_Shift/GetDutiesInShift/{areaId}")]
        [HttpGet]
        public Dictionary<string, List<DutyInShift>> GetDutiesInShift(int areaId)
        {
            DateTime date = DateTime.Today.AddMonths(1);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            Area area = new Area();
            Dictionary <string,List < DutyInShift >> AssignToShift = new Dictionary<string,List < DutyInShift >> ();
            for (var day = firstDayOfMonth.Date; day.Date <= lastDayOfMonth.Date; day = day.AddDays(1))
            {
                string dayS = (day.ToString("yyyy-MM-dd"));
                List<DutyInShift> dutiesInShift;
                dutiesInShift = area.ReadDutiesInShift(dayS);
                AssignToShift.Add(dayS, dutiesInShift);
            }
            return AssignToShift;
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
            Day_In_Shift shift = new Day_In_Shift();
            DateTime date = DateTime.Today.AddMonths(1);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            shift.DeleteExistAssign(firstDayOfMonth, lastDayOfMonth);
            Area area = new Area();
            var blocks = area.GetBlocksOfArea(areaId);
            for (var day = firstDayOfMonth.Date; day.Date <= lastDayOfMonth.Date; day = day.AddDays(1))
            {
                shift.Shift_Date = day;
                foreach (Block block in blocks)
                {
                    List<RequirementForSpecificShift> permantReqs = shift.GetPermantReq(block.Block_Id);
                    List<RequirementForSpecificShift> speciaelReqs = shift.GetSpeciaelReq(block.Block_Id, shift.Shift_Date);
                    AssignToShift(permantReqs, shift, areaId, block);
                    AssignToShift(speciaelReqs, shift, areaId, block);
                }
            }
        }

        private List<DutyInShift> AssignToShift(List<RequirementForSpecificShift> reqs, Day_In_Shift shift,int areaId,Block block)
        {
            List<DutyInShift> dutyInShifts = new List<DutyInShift>();
            foreach (var req in reqs)
            {
                for (int i = 0; i < req.Quantity; i++)
                {
                    Employee matchEmployee = shift.FindMatchEmployee(areaId,shift.Shift_Date,req.Requirement_Id);
                    if (matchEmployee.Id != null)
                        shift.InsertEmployeeToShift(matchEmployee.Id, block.Block_Id, shift.Shift_Date, req.Requirement_Id);
                    else
                        break;
                }
            }
            return dutyInShifts;
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