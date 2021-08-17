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
        int iteration_Number;


        public Day_In_Shift() { }

        public Day_In_Shift(string id, int block_Id, DateTime shift_Date, int requirement_Id,int iteration_Number)
        {
            Id = id;
            Block_Id = block_Id;
            Shift_Date = shift_Date;
            Requirement_Id = requirement_Id;
            Iteration_Number = iteration_Number;
        }

        public string Id { get => id; set => id = value; }
        public int Block_Id { get => block_Id; set => block_Id = value; }
        public DateTime Shift_Date { get => shift_Date; set => shift_Date = value; }
        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }
        public int Iteration_Number { get => iteration_Number; set => iteration_Number = value; }


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

        public Employee FindMatchEmployee(int areaId, DateTime Shift_Date,int Requirement_Id,int iteration_Number)
        {
            DBServices dbs = new DBServices();
            Employee matchEmployee = dbs.FindMatchEmployee(areaId, "want", Shift_Date, Requirement_Id, iteration_Number);
            if (matchEmployee.Id==null)
                matchEmployee = dbs.FindMatchEmployee(areaId, "can", Shift_Date, Requirement_Id, iteration_Number);
            return matchEmployee;
        }

        public void InsertEmployeeToShift (string id,int blockId,DateTime shift_Date,int Requirement_Id,int iteration_Number)
        {
            DBServices dbs = new DBServices();
            dbs.InsertEmployeeToShift(id, blockId, shift_Date, Requirement_Id, iteration_Number);
        }

        public void DeleteExistAssign(int areaId, DateTime startDate, DateTime endDate)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteExistAssign(areaId, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        }
        public void DeleteAllAssignsExceptBest(int areaId, DateTime startDate, DateTime endDate,int bestIteration)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteAllAssignsExceptBest(areaId, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), bestIteration);
        }
        
        public int GetBestIteration(int month, int areaId,int iterationsNum,double satisfactionPrecent, double fairnessPrecent)
        {
            DBServices dbs = new DBServices();

            List<double> satisfactionList = new List<double>();
            List<double> fairnessList=new List<double>();
            for (int i=0;i< iterationsNum;i++)
            {
                List<double> arr = dbs.GetScoreHelper(areaId, month, i + 1);
                satisfactionList.Add(arr[0]);
                fairnessList.Add(arr[1]);
            }
            double maxSatis = satisfactionList.Max();
            double minSatis = satisfactionList.Min();
            double maxFair = fairnessList.Max();
            double minFair = fairnessList.Min();
            double maxScore = 0;
            int bestIteration = 1;
            List<double> temp = new List<double>();

            for (int i = 0; i < iterationsNum; i++)
            {
                double satsifNormalize = (satisfactionList[i] - minSatis) / (maxSatis - minSatis);
                double fairNormalize = (fairnessList[i] - minFair) / (maxFair - minFair);
                double fs = satisfactionPrecent * satsifNormalize + (fairnessPrecent * (1-fairNormalize));
                temp.Add(fs);
                if (fs>maxScore)
                {
                    maxScore = fs;
                    bestIteration = i + 1;
                }
            }
            return bestIteration;
        }
    }
}
