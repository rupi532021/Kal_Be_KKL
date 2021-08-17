using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Substitution_Request
    {
        int request_Number;
        string id_From;
        string name_From;
        string id_To;
        string name_To;
        string requirement_Name;
        DateTime request_date;
        int status;
        public Substitution_Request(int request_Number, string id_From, string name_From, string id_To, string name_To, string requirement_Name, DateTime request_date, int status)
        {
            Request_Number = request_Number;
            Id_From = id_From;
            Name_From = name_From;
            Id_To = id_To;
            Name_To = name_To;
            Requirement_Name = requirement_Name;
            Request_date = request_date;
            Status = status;
        }

        public Substitution_Request()
        {

        }

        public int Request_Number { get => request_Number; set => request_Number = value; }
        public string Id_From { get => id_From; set => id_From = value; }
        public string Name_From { get => name_From; set => name_From = value; }
        public string Id_To { get => id_To; set => id_To = value; }
        public string Name_To { get => name_To; set => name_To = value; }
        public string Requirement_Name { get => requirement_Name; set => requirement_Name = value; }
        public DateTime Request_date { get => request_date; set => request_date = value; }
        public int Status { get => status; set => status = value; }

        public void insertSubstitutionRequest()
        {
            DBServices dbs = new DBServices();
            dbs.insertSubstitutionRequest(this);
        }

        public List<Substitution_Request> Read_Area_Substitution_Request(int areaId)
        {
            DBServices dbs = new DBServices();
            List<Substitution_Request> sReqList = dbs.Read_Area_Substitution_Request(areaId);
            return sReqList;
        }
        public List<Substitution_Request> Read_Region_Substitution_Request(int regionId)
        {
            DBServices dbs = new DBServices();
            List<Substitution_Request> sReqList = dbs.Read_Region_Substitution_Request(regionId);
            return sReqList;
        }

        public void ApproveRequest()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateSubstitution(Id_From, Id_To, Request_date);
            dbs.UpdateStatus(Request_Number, 1);
        }  
        
        public void RejectRequest()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateStatus(Request_Number, 2);
        }
    }
}