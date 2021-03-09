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
        int level;
        bool deleted;

        public Employee()
        {

        }

        public Employee(string id, string first_Name, string last_Name, string gender, DateTime birth_Date, string phone_Number, string mail, string password, int level, bool deleted)
        {
            Id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Gender = gender;
            Birth_Date = birth_Date;
            Phone_Number = phone_Number;
            Mail = mail;
            Password = password;
            Level = level;
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
        public int Level { get => level; set => level = value; }
        public bool Deleted { get => deleted; set => deleted = value; }

        public Employee LogIn(string id, string password)
        {
            DBServices dbs = new DBServices();
            Employee e = dbs.LogIn(id, password);
            return e;
        }
        public void Insert_Employee() 
        {
            DBServices dbl = new DBServices();
            dbl.Insert_Employee(this);
        }
    }
}