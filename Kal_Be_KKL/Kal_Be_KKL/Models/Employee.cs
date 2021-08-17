using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kal_Be_KKL.Models.DAL;


namespace Kal_Be_KKL.Models
{
    public class Employee
    {
        string id;
        string first_Name;
        string last_Name;
        string gender;
        DateTime birth_Date;
        string phone_Number;
        string mail;
        string password;
        bool deleted;

        public Employee()
        {

        }

        public Employee(string id, string first_Name, string last_Name, string gender, DateTime birth_Date, string phone_Number, string mail, string password, bool deleted)
        {
            Id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Gender = gender;
            Birth_Date = birth_Date;
            Phone_Number = phone_Number;
            Mail = mail;
            Password = password;
            Deleted = deleted;
        }

        public string Id { get => id; set => id = value; }
        public string First_Name { get => first_Name; set => first_Name = value; }
        public string Last_Name { get => last_Name; set => last_Name = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime Birth_Date { get => birth_Date; set => birth_Date = value; }
        public string Phone_Number { get => phone_Number; set => phone_Number = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public bool Deleted { get => deleted; set => deleted = value; }

        public Employee LogIn(string id, string password)
        {
            DBServices dbs = new DBServices();
            Employee e = dbs.LogIn(id, password);
            return e;
        }     
        public List<Employee> GetAllEmployee()
        {
            DBServices dbs = new DBServices();
            List<Employee> emps = dbs.GetAllEmployee();
            return emps;
        }     
        public List<Employee> GetAllPossibleEmployees(DateTime exdate, int myJob, int area_id)
        {
            DBServices dbs = new DBServices();
            List<Employee> emps = dbs.GetAllPossibleEmployees(exdate,myJob,area_id);
            return emps;
        }
        public void Insert_Employee() 
        {
            DBServices dbl = new DBServices();
            dbl.Insert_Employee(this);
        }

        public bool insert_course_of_duty(Courses_Of_Duty [] cod) 
        {
            DBServices dbl = new DBServices();
            return dbl.insert_course_of_duty(cod);
                
            
        }

        public Worker_In_Area getAreaAndJob(string id)
        {
            DBServices dbs = new DBServices();
            Worker_In_Area wia = dbs.getAreaAndJob(id);
            return wia;
        }
        public void Edit_Employee()
        {
            DBServices dbs = new DBServices();
            dbs.Edit_Employee(this);
        }


        public Worker_In_Region getRegionAndJob(string id)
        {
            DBServices dbs = new DBServices();
            Worker_In_Region wir = dbs.getRegionAndJob(id);
            return wir;
        }
    }
}