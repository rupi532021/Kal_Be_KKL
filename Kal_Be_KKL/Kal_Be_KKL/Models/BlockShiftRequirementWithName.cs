using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class BlockShiftRequirementWithName
    {
        string requirement_Name;
        int quantity;
        string comments;
        int requirement_Id;
        public BlockShiftRequirementWithName()
        {

        }

        public BlockShiftRequirementWithName(string requirement_Name, int quantity, string comments, int requirement_Id)
        {
            Requirement_Name = requirement_Name;
            Quantity = quantity;
            Comments = comments;
            Requirement_Id = requirement_Id;
        }

        public string Requirement_Name { get => requirement_Name; set => requirement_Name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Comments { get => comments; set => comments = value; }
        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }

        public List<BlockShiftRequirementWithName> Read_PermanentRequirement(int blockId)
        {
            DBServices dbs = new DBServices();
            List<BlockShiftRequirementWithName> PermanentRequirements_List = dbs.Read_PermanentRequirement(blockId);
            return PermanentRequirements_List;
        }

        public List<BlockShiftRequirementWithName> Read_SpecialRequirement(int blockId, DateTime shiftDate)
        {
            DBServices dbs = new DBServices();
            List<BlockShiftRequirementWithName> SpecialRequirement_List = dbs.Read_SpecialRequirement(blockId, shiftDate);
            return SpecialRequirement_List;
        }
        public void Delete_SpecialRequirement(int blockId, DateTime shiftDate, int Requirement_Id)
        {
            DBServices dbs = new DBServices();
            dbs.Delete_SpecialRequirement(blockId, shiftDate, Requirement_Id);

        }
    }
}





