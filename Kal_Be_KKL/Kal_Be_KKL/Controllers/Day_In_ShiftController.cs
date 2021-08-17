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

        [Route("api/Day_In_Shift/GetDutiesInShiftToday/{areaId}/")]
        [HttpGet]
        public List<DutyInShift> GetDutiesInShiftToday(int areaId)
        {
            Area area = new Area();
            string dayS = DateTime.Today.ToString("yyyy-MM-dd");
            List<DutyInShift> dutiesInShift = area.ReadDutiesInShift(dayS, areaId);
            return dutiesInShift;
        }

        // GET api/<controller>/5
        [Route("api/Day_In_Shift/GetDutiesInShift/{areaId}/{isNextMonth}")]
        [HttpGet]
        public Dictionary<string, List<DutyInShift>> GetDutiesInShift(int areaId,bool isNextMonth)
        {
            DateTime date = DateTime.Today.AddMonths(isNextMonth ? 1 : 0);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            Area area = new Area();
            Dictionary<string, List<DutyInShift>> AssignToShift = new Dictionary<string, List<DutyInShift>>();
            for (var day = firstDayOfMonth.Date; day.Date <= lastDayOfMonth.Date; day = day.AddDays(1))
            {
                string dayS = day.ToString("yyyy-MM-dd");
                List<DutyInShift> dutiesInShift;
                dutiesInShift = area.ReadDutiesInShift(dayS, areaId);
                AssignToShift.Add(dayS, dutiesInShift);
            }
            return AssignToShift;
        }

        // POST api/<controller>
        public void Post(Day_In_Shift dayinshift)
        {
            dayinshift.InsertDayInShift();
        }
        [Route("api/Day_In_Shift/SmartPost/{areaId}/{fairness}/{satisfaction}")]
        [HttpPost]
        public void SmartPost(int areaId,double fairness, double satisfaction)
        {
            const int ITERATIONS = 3;
            Day_In_Shift shift = new Day_In_Shift();
            DateTime date = DateTime.Today.AddMonths(1);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            shift.DeleteExistAssign(areaId, firstDayOfMonth, lastDayOfMonth);
            Area area = new Area();
            var blocks = area.GetBlocksOfArea(areaId);

            List<DateTime> allDays = new List<DateTime>();
            for (var day = firstDayOfMonth.Date; day.Date <= lastDayOfMonth.Date; day = day.AddDays(1))
            {
                allDays.Add(day);
            }

            Random rnd = new Random();
            for (int i = 1; i <= ITERATIONS; i++)
            {
                List<DateTime> allDaysCopy = new List<DateTime>(allDays);
                while (allDaysCopy.Count>0)
                {
                    int index = rnd.Next(allDaysCopy.Count);
                    shift.Shift_Date = allDaysCopy[index];
                    foreach (Block block in blocks)
                    {
                        List<RequirementForSpecificShift> permantReqs = shift.GetPermantReq(block.Block_Id);
                        List<RequirementForSpecificShift> speciaelReqs = shift.GetSpeciaelReq(block.Block_Id, shift.Shift_Date);
                        AssignToShift(permantReqs, shift, areaId, block, i);
                        AssignToShift(speciaelReqs, shift, areaId, block, i);
                    }
                    allDaysCopy.RemoveAt(index);
                }
               
            }
            int bestIteration = shift.GetBestIteration(date.Month,areaId, ITERATIONS,satisfaction/100,fairness/100);
            shift.DeleteAllAssignsExceptBest(areaId, firstDayOfMonth, lastDayOfMonth, bestIteration);
        }




        private void AssignToShift(List<RequirementForSpecificShift> reqs, Day_In_Shift shift, int areaId, Block block, int iteration_Number)
        {
            foreach (var req in reqs)
            {
                for (int i = 0; i < req.Quantity; i++)
                {
                    Employee matchEmployee = shift.FindMatchEmployee(areaId, shift.Shift_Date, req.Requirement_Id, iteration_Number);
                    if (matchEmployee.Id != null)
                        shift.InsertEmployeeToShift(matchEmployee.Id, block.Block_Id, shift.Shift_Date, req.Requirement_Id, iteration_Number);
                    else
                        break;
                }
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