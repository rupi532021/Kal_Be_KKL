using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class RequirementForSpecificShift
    {
        int requirement_Id;
        int quantity;

        public RequirementForSpecificShift(int requirement_Id, int quantity)
        {
            Requirement_Id = requirement_Id;
            Quantity = quantity;
        }

        public RequirementForSpecificShift()
        {
            
        }

        public int Requirement_Id { get => requirement_Id; set => requirement_Id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}