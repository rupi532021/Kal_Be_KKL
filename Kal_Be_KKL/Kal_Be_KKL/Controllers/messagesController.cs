using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class messagesController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                Message msg = new Message();
                List<Message> messages = msg.Read_Messages();
                return Request.CreateResponse(HttpStatusCode.OK, messages);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Message msg)
        {
            try
            {
                msg.Insert_Message();
                return Request.CreateResponse(HttpStatusCode.OK, "בוצע");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}