using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Course
    {
        int course_Id;
        string course_Name;
        string description;
        int validity;

        public Course(int course_Id, string course_Name, string description, int validity)
        {
            Course_Id = course_Id;
            Course_Name = course_Name;
            Description = description;
            Validity = validity;
        }

        public int Course_Id { get => course_Id; set => course_Id = value; }
        public string Course_Name { get => course_Name; set => course_Name = value; }
        public string Description { get => description; set => description = value; }
        public int Validity { get => validity; set => validity = value; }
        public Course() { }
        public List<Course> Read_Courses()
        {
            DBServices dbs = new DBServices();
            List<Course> Course_List = dbs.Read_Courses();
            return Course_List;
        } 

    }
}