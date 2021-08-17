using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class ShiftRequirement
    {
        int requirement_Id;
        string requirement_Name;

        public ShiftRequirement(int requirement_Id, string requirement_Name)
        {
            Requirement_Id = requirement_Id;
            Requirement_Name = requirement_Name;
        }

        public ShiftRequirement()
        {

        }

        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }
        public string Requirement_Name { get => requirement_Name; set => requirement_Name = value; }

        public List<ShiftRequirement> Read_Requirements()
        {
            DBServices dbs = new DBServices();
            List<ShiftRequirement> requirements_List = dbs.Read_Requirements();
            return requirements_List;
        }

        public int GetJobId(string jobname) 
        {
            DBServices dbs = new DBServices();
            return dbs.GetJobId(jobname);
        }
    }
}