using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class DutyInShift
    {
        string first_Name;
        string last_Name;
        string requirement_Name;
        string block_Name;
        string phone;

        public DutyInShift(string first_Name, string last_Name, string requirement_Name, string block_Name, string phone)
        {
            Phone = phone;
            First_Name = first_Name;
            Last_Name = last_Name;
            Requirement_Name = requirement_Name;
            Block_Name = block_Name;
        }

        public DutyInShift()
        {

        }
        public string Phone { get => phone; set => phone = value; }
        public string First_Name { get => first_Name; set => first_Name = value; }
        public string Last_Name { get => last_Name; set => last_Name = value; }
        public string Requirement_Name { get => requirement_Name; set => requirement_Name = value; }
        public string Block_Name { get => block_Name; set => block_Name = value; }
    }
}