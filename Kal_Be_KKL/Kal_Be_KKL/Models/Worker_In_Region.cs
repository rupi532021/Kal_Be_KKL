using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kal_Be_KKL.Models.DAL;
namespace Kal_Be_KKL.Models
{
    public class Worker_In_Region
    {
        string id;
        int region_Id;
        int job_Id;
        DateTime job_Start_Date;

        public Worker_In_Region(string id, int region_Id, int job_Id, DateTime job_Start_Date)
        {
            Id = id;
            Region_Id = region_Id;
            Job_Id = job_Id;
            Job_Start_Date = job_Start_Date;
        }

        public string Id { get => id; set => id = value; }
        public int Region_Id { get => region_Id; set => region_Id = value; }
        public int Job_Id { get => job_Id; set => job_Id = value; }
        public DateTime Job_Start_Date { get => job_Start_Date; set => job_Start_Date = value; }
        public Worker_In_Region() { }
        public void Insert_Worker_In_Region()
        {
            DBServices dbl = new DBServices();
            dbl.Insert_Worker_In_Region(this);
        }
    }
}