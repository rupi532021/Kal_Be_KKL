using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Block
    {
        int area_Id;
        int block_Id;
        string block_Name;

        public Block(int area_Id, int block_Id, string block_Name)
        {
            Area_Id = area_Id;
            Block_Id = block_Id;
            Block_Name = block_Name;
        }

        public Block()
        {

        }

        public int Area_Id { get => area_Id; set => area_Id = value; }
        public int Block_Id { get => block_Id; set => block_Id = value; }
        public string Block_Name { get => block_Name; set => block_Name = value; }

        public List<Block> Read_Blocks()
        {
            DBServices dbs = new DBServices();
            List<Block> blocks_List = dbs.Read_Blocks();
            return blocks_List;
        }
        public List<Block> Read_Blocks_With_Area_Id(int Area_Id)
        {
            DBServices dbs = new DBServices();
            List<Block> blocks_List = dbs.Read_Blocks_With_Area_Id(Area_Id);
            return blocks_List;
        }
    }
}