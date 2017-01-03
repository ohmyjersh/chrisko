using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ChrisKo.Models { 
    public class Chrisko {
        [BsonId]
        public string Id {get; set;}
        public string Url {get; set;}
        public int Visits {get; set;} = 0;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}