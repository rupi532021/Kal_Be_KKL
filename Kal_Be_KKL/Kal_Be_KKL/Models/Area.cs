using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Area
    {
        int region_Id;
        int area_Id;
        string area_Name;
        string manager_Id;
        string patrol_Id;
        string forester_Id;

        public Area(int region_Id, int area_Id, string area_Name, string manager_Id, string patrol_Id, string forester_Id)
        {
            Region_Id = region_Id;
            Area_Id = area_Id;
            Area_Name = area_Name;
            Manager_Id = manager_Id;
            Patrol_Id = patrol_Id;
            Forester_Id = forester_Id;
        }

        public int Region_Id { get => region_Id; set => region_Id = value; }
        public int Area_Id { get => area_Id; set => area_Id = value; }
        public string Area_Name { get => area_Name; set => area_Name = value; }
        public string Manager_Id { get => manager_Id; set => manager_Id = value; }
        public string Patrol_Id { get => patrol_Id; set => patrol_Id = value; }
        public string Forester_Id { get => forester_Id; set => forester_Id = value; }
        public List<Area> Read_Area() 
        {
            DBServices dbs = new DBServices();
            List<Area> Area_List = dbs.Read_Area();
                return Area_List;
        }
        public Area() { }
    }
}