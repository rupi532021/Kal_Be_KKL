using Kal_Be_KKL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kal_Be_KKL.Controllers
{
    public class BlocksController : ApiController
    {
        // GET api/<controller>
        public List<Block> Get()
        {
            Block block = new Block();
            List<Block> blocksList = block.Read_Blocks();
            return blocksList;
        }

        // GET api/<controller>/5
        [Route("api/Blocks/Get_Blocks_With_Area_Id/{Area_Id}")]
        [HttpGet]
        public List<Block> Get(int Area_Id)
        {
            Block block = new Block();
            List<Block> blocksList = block.Read_Blocks_With_Area_Id(Area_Id);
            return blocksList;
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