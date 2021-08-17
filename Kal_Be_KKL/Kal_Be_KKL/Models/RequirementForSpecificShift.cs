using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class RequirementForSpecificShift
    {
        int requirement_Id;
        string requirement_Name;
        int quantity;

        public RequirementForSpecificShift(int requirement_Id, string requirement_Name , int quantity)
        {
            Requirement_Id = requirement_Id;
            Requirement_Name = requirement_Name;
            Quantity = quantity;
        }

        public RequirementForSpecificShift()
        {
            
        }

        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Requirement_Name { get => requirement_Name; set => requirement_Name = value; }
    }
}