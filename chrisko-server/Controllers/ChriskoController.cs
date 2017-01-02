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
    public class ChriskoController : Controller
    {
        private RedisCache Store;
        public ChriskoController() {
            Console.WriteLine("Connecting to cache");
            Store = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:6379",
                InstanceName = "chrisko"
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var storedChrisko =  GetChrisko(id);
            if (storedChrisko != null)
            {
                // Update store with increased Visits
                var newChrisko = GenerateChrisko(storedChrisko.Url, storedChrisko.Id, storedChrisko.Visits + 1);
                SaveOrUpdateStore(newChrisko);
                //return Redirect(storedChrisko.Url);
                return Redirect("http://google.com");
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ChriskoRequest request)
        {
            Console.WriteLine(request.Url);
            var chrisko = GenerateChrisko(request.Url, Guid.NewGuid().ToString().Substring(0,6));
            SaveOrUpdateStore(chrisko);

            var storedChrisko = GetChrisko(chrisko.Id);
            if (storedChrisko == null) {
                return BadRequest();
            }
            return Json(storedChrisko);
        }

        public Chrisko GenerateChrisko(string url, string id, int visits = 0) {
            return new Chrisko {
                Id = id,
                Url = url,
                Visits = visits
            };
        }

        public void SaveOrUpdateStore(Chrisko chrisko) {
            var output = JsonConvert.SerializeObject(chrisko);
            var encodedChrisko = Encoding.UTF8.GetBytes(output);
            Store.Set(chrisko.Id,encodedChrisko, new DistributedCacheEntryOptions());
        }

        public Chrisko GetChrisko(string key) {
            var value = Store.Get(key);
            if(value == null) {
                return null;
            }
            var encoded = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<Chrisko>(encoded);
        }
    }
}
