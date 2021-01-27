using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        //From-Route
        [HttpGet("{id}")]
        public string GetById(int ID)
        {
            return $"Number ID Arezoo is : {ID}";
        }
        //Receives String From Query
        [HttpGet("From-Query")]
        public IActionResult GetFromQuery([FromQuery]string Name)
        {
            return Content("My Name is " + Name);
        }
        //FromHeader
        [HttpPost("From-Header")]
        public IActionResult GetData([FromHeader(Name ="Accept-Language")]string accesstoken,[FromHeader(Name ="User-Agent")]string agent)
        {
            return Content("accesstoken=" + accesstoken + "" + agent);
        }
    }
}
