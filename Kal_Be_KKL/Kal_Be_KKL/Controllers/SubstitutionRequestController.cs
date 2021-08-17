using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class SubstitutionRequestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Route("api/SubstitutionRequest/GetAreaRequests/{areaId}")]
        public List<Substitution_Request> GetAreaRequests(int areaId)
        {
            Substitution_Request sReq = new Substitution_Request();
            List<Substitution_Request> sReqList = sReq.Read_Area_Substitution_Request(areaId);
            return sReqList;
        }

        [Route("api/SubstitutionRequest/GetRegionRequests/{regionId}")]
        public List<Substitution_Request> GetRegionRequests(int regionId)
        {
            Substitution_Request sReq = new Substitution_Request();
            List<Substitution_Request> sReqList = sReq.Read_Region_Substitution_Request(regionId);
            return sReqList;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Substitution_Request sReq)
        {
            try
            {
                sReq.insertSubstitutionRequest();
                return Request.CreateResponse(HttpStatusCode.OK, "בוצע");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "מצטערים קיימת בעיה במערכת יש לנסות שוב במועד מאוחר יותר");
            }
        }

        // PUT api/<controller>/5
        [Route("api/SubstitutionRequest/approveRequest")]
        public void PutApproveRequest([FromBody] Substitution_Request sReq)
        {
            sReq.ApproveRequest();
        }

        [Route("api/SubstitutionRequest/RejectRequest")]
        public void PutRejectRequest([FromBody] Substitution_Request sReq)
        {
            sReq.RejectRequest();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}