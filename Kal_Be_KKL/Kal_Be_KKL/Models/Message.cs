using Kal_Be_KKL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kal_Be_KKL.Models
{
    public class Message
    {
        int message_Number;
        string creator_Id;
        DateTime creation_Date;
        string title;
        string content;

        public Message(int message_Number, string creator_Id, DateTime creation_Date, string title, string content)
        {
            Message_Number = message_Number;
            Creator_Id = creator_Id;
            Creation_Date = creation_Date;
            Title = title;
            Content = content;
        }
        
        public Message()
        {

        }

        public int Message_Number { get => message_Number; set => message_Number = value; }
        public string Creator_Id { get => creator_Id; set => creator_Id = value; }
        public DateTime Creation_Date { get => creation_Date; set => creation_Date = value; }
        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }

        public void Insert_Message()
        {
            DBServices dbs = new DBServices();
            dbs.Insert_Message(this);
        }

        public List<Message> Read_Messages()
        {
            DBServices dbs = new DBServices();
            List<Message> messages = dbs.Read_Messages();
            return messages;
        }
    }
}