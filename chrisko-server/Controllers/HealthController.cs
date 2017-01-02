using System;
using System.Text;
using ChrisKo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;

namespace ChrisKo.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    { 
                // GET api/values/5
        [HttpGet("/")]
        public IActionResult Get(string id)
        { 
            return new OkResult();
        }
    }
}