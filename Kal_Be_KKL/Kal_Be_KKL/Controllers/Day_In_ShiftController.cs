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