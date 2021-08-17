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
        public List<Block> GetBlocksOfArea(int areaId)
        {
            DBServices dbs = new DBServices();
            List<Block> block_list = dbs.Read_Blocks_With_Area_Id(areaId);
            return block_list;
        } 
        public List<Area> GetAreasOfRegion(int region_id)
        {
            DBServices dbs = new DBServices();
            List<Area> area_list = dbs.GetAreasOfRegion(region_id);
            return area_list;
        }

        public Area Read_Area_By_Emp_Id (string id)
        {
            DBServices dbs = new DBServices();
            Area area = dbs.Read_Area_By_Emp_Id(id);
            return area;
        }

        public List<DutyInShift> ReadDutiesInShift (string date, int areaId)
        {
            DBServices dbs = new DBServices();
            List<DutyInShift> dutiesInShift = dbs.ReadDutiesInShift(date, areaId);
            return dutiesInShift;
        }
    }
}