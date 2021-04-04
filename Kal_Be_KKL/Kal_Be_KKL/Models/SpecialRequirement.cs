using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class SpecialRequirement
    {
        int block_Id;
        DateTime shift_Date;
        int requirement_Id;
        int quantity;
        string comments;

        public SpecialRequirement(int block_Id, DateTime shift_Date, int requirement_Id, int quantity, string comments)
        {
            Block_Id = block_Id;
            Shift_Date = shift_Date;
            Requirement_Id = requirement_Id;
            Quantity = quantity;
            Comments = comments;
        }

        public SpecialRequirement()
        {

        }

        public int Block_Id { get => block_Id; set => block_Id = value; }
        public DateTime Shift_Date { get => shift_Date; set => shift_Date = value; }
        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Comments { get => comments; set => comments = value; }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertSpecialRequirement(this);
        } 
        public void Update_SpecialRequirement()
        {
            DBServices dbs = new DBServices();
            dbs.Update_SpecialRequirement(this);
        }
        public int If_SpecialRequirement_Is_Exist(SpecialRequirement specialRequirement) 
        {
            DBServices dbs = new DBServices();
            return  dbs.If_SpecialRequirement_Is_Exist(specialRequirement);
        }
    }
}