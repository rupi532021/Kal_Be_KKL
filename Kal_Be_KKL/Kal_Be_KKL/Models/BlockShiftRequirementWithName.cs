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

        public BlockShiftRequirementWithName()
        {

        }

        public BlockShiftRequirementWithName(string requirement_Name, int quantity, string comments)
        {
            Requirement_Name = requirement_Name;
            Quantity = quantity;
            Comments = comments;
        }

        public string Requirement_Name { get => requirement_Name; set => requirement_Name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Comments { get => comments; set => comments = value; }

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
    }
}