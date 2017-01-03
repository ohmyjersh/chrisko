using System;
using System.Text;
using ChrisKo.Models;
using ChrisKo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;

namespace ChrisKo.Controllers
{
    [Route("api/[controller]")]
    public class ChriskoController : Controller
    {
        private readonly IChriskoRepository _chriskoRepository;
        private RedisCache Store;
        public ChriskoController(IChriskoRepository chriskoRepository) {
            _chriskoRepository = chriskoRepository;
            Console.WriteLine("Connecting to cache");
            Store = new RedisCache(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:6379",
                InstanceName = "chrisko"
            });
        }

        // GET api/values/5
        [HttpGet("{shortUrl}")]
        public IActionResult Get(string shortUrl)
        { 
            Console.WriteLine("shortUrl:{0}",shortUrl);
            var storedChrisko =  GetChrisko(shortUrl);
            if (storedChrisko != null)
            {
                // Update store with increased Visits
                var newChrisko = GenerateChrisko(storedChrisko.Url, storedChrisko.shortUrl, storedChrisko.Visits + 1);
                SaveOrUpdateStore(newChrisko);
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
            var chrisko = GenerateChrisko(GetUri(request.Url).AbsoluteUri, Guid.NewGuid().ToString().Substring(0,6));
            _chriskoRepository.AddChriskoAsync(chrisko);
            //SaveOrUpdateStore(chrisko);

            //var storedChrisko = GetChrisko(chrisko.shortUrl);
            var storedChrisko = _chriskoRepository.GetChriskoByIdAsync(chrisko.Id);
            if (storedChrisko == null) {
                return BadRequest();
            }
            return Json(storedChrisko);
        }

        public Chrisko GenerateChrisko(string url, string shortUrl, int visits = 0) {
            return new Chrisko {
                shortUrl = shortUrl,
                Url = url,
                Visits = visits
            };
        }

        public Uri GetUri(string s)
        {
            return new UriBuilder(s).Uri;
        }

        public void SaveOrUpdateStore(Chrisko chrisko) {
            var output = JsonConvert.SerializeObject(chrisko);
            var encodedChrisko = Encoding.UTF8.GetBytes(output);
            Store.Set(chrisko.shortUrl, encodedChrisko, new DistributedCacheEntryOptions());
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
