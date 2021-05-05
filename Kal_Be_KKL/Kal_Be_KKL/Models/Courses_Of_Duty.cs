using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Courses_Of_Duty
    {
        string id;
        int course_Id;
        DateTime receipt_Course_Date;

        public Courses_Of_Duty(string id, int course_Id, DateTime receipt_Course_Date)
        {
            Id = id;
            Course_Id = course_Id;
            Receipt_Course_Date = receipt_Course_Date;
        }

        public string Id { get => id; set => id = value; }
        public int Course_Id { get => course_Id; set => course_Id = value; }
        public DateTime Receipt_Course_Date { get => receipt_Course_Date; set => receipt_Course_Date = value; }

        public Courses_Of_Duty() { }

        public List<Courses_Of_Duty> GetAllCoursesOfDuty(string id)
        {
            DBServices dbs = new DBServices();
            List<Courses_Of_Duty> Courses_Of_Duty_List = dbs.GetAllCoursesOfDuty(id);
            return Courses_Of_Duty_List;
        }
    }
}