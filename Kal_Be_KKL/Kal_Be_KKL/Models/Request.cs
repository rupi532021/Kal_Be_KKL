using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Request
    {
        string id;
        DateTime request_Date;
        string request_Status;

        public Request(string Id, DateTime request_Date, string request_Status)
        {
            Id = id;
            Request_Date = request_Date;
            Request_Status =  request_Status;
            
        }

        public string Id { get => id; set => id = value; }
        public DateTime Request_Date { get => request_Date; set => request_Date = value; }
        public string Request_Status { get => request_Status; set => request_Status = value; }

        public Request() { }
        public void Insert() 
        {
            DBServices dbs = new DBServices();
            dbs.Insert_Requests(this);
        }
        public void delete_requests(string id, int month) {
            DBServices dbs = new DBServices();
            dbs.Delete_Request(id, month);
        }
    }
}