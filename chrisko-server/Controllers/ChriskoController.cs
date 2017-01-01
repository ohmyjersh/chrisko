using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                var newChrisko = IncreaseVisit(storedChrisko);
                var output = JsonConvert.SerializeObject(newChrisko);
                var encodedChrisko = Encoding.UTF8.GetBytes(output);
                Store.Set(newChrisko.Id, encodedChrisko);


                return Redirect(storedChrisko.Url);
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
            var chrisko = GenerateChrisko(request.Url, Guid.NewGuid().ToString().Substring(0,6));
            SaveOrUpdateStore(chrisko);

            var storedChrisko = GetChrisko(chrisko.Id);
            if (storedChrisko == null) {
                return BadRequest();
            }
            return Json(storedChrisko);
        }

        public Chrisko IncreaseVisit(Chrisko oldChrisko) {
            return new Chrisko {
                Url = oldChrisko.Url,
                Id = oldChrisko.Id,
                Visits = oldChrisko.Visits + 1
            };
        }

        public Chrisko GenerateChrisko(string url, string id, int visits = 0) {
            return new Chrisko {
                Id = id,
                Url = url,
                Visits = 0
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
            var serialized = JsonConvert.DeserializeObject<Chrisko>(encoded);
            return serialized;
        }
    }
}
