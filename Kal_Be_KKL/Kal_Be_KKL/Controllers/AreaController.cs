using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class AreaController : ApiController
    {
        // GET api/<controller>
        public List<Area> Get()
        {
            Area area = new Area();
            List<Area> AreaList = area.Read_Area();
            return AreaList;
        }

        // GET api/<controller>/5
        [Route("api/Area/GetAreaByEmpId/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAreaByEmpId(string id)
        {
            Area area = new Area();
            area = area.Read_Area_By_Emp_Id(id);
            if (area.Area_Id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id incorrect");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, area);
            }
        }

        [Route("api/Area/GetAreaBlcoks/{id}")]
        [HttpGet]
        public List<Block> GetAreaBlcoks(int id)
        {
            Area area = new Area();
            List<Block> blocks = area.GetBlocksOfArea(id);
            return blocks;
        }    
        [Route("api/Area/AreaWithDistrict/{region_id}")]
        [HttpGet]
        public List<Area> AreaWithDistrict(int region_id)
        {
            Area area = new Area();
            List<Area> blocks = area.GetAreasOfRegion(region_id);
            return blocks;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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