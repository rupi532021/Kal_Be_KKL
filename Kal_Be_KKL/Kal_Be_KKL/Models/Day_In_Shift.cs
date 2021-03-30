using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Day_In_Shift
    {
        string id;
        int block_Id;
        DateTime shift_Date;
        int requirement_Id;


        public Day_In_Shift() { }

        public Day_In_Shift(string id, int block_Id, DateTime shift_Date, int requirement_Id)
        {
            Id = id;
            Block_Id = block_Id;
            Shift_Date = shift_Date;
            Requirement_Id = requirement_Id;
        }

        public string Id { get => id; set => id = value; }
        public int Block_Id { get => block_Id; set => block_Id = value; }
        public DateTime Shift_Date { get => shift_Date; set => shift_Date = value; }
        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }

        public void InsertDayInShift()
        {
            DBServices dbs = new DBServices();
            dbs.InsertDayInShift(this);
        }

        public List<Day_In_Shift> ReadDayInShift()
        {
            DBServices dbs = new DBServices();
            List<Day_In_Shift> DayInShiftList = dbs.ReadDayInShift();
            return DayInShiftList;
        }

        public List<RequirementForSpecificShift> GetPermantReq(int blockId)
        {
            DBServices dbs = new DBServices();
            List<RequirementForSpecificShift> permantReqListForBlock = dbs.ReadPermantReqListForBlock(blockId);
            return permantReqListForBlock;
        }  

        public List<RequirementForSpecificShift> GetSpeciaelReq(int blockId,DateTime Shift_Date)
        {
            DBServices dbs = new DBServices();
            List<RequirementForSpecificShift> speciaelReqListForBlock = dbs.ReadSpeciaelReqListForBlock(blockId, Shift_Date);
            return speciaelReqListForBlock;
        }

        public Employee FindMatchEmployee(int areaId, DateTime Shift_Date,int Requirement_Id)
        {
            DBServices dbs = new DBServices();
            Employee matchEmployee = dbs.FindMatchEmployee(areaId, "want", Shift_Date, Requirement_Id);
            if (matchEmployee.Id==null)
                matchEmployee = dbs.FindMatchEmployee(areaId, "can", Shift_Date, Requirement_Id);
            return matchEmployee;
        }

        public void InsertEmployeeToShift (string id,int blockId,DateTime shift_Date,int Requirement_Id)
        {
            DBServices dbs = new DBServices();
            dbs.InsertEmployeeToShift(id, blockId, shift_Date, Requirement_Id);
        }
    }
}
