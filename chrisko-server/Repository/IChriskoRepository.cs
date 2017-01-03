using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChrisKo.Models;

namespace ChrisKo.Repository
{
    public class ChriskoRepository : IChriskoRepository
    {
        public Task AddChrisko(Chrisko chrisko)
        {
            throw new NotImplementedException();
        }

        public Task<Chrisko> GetChriskoByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public Task<Chrisko> GetChriskoByUrlId(string urlId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNote(string id, Chrisko chrisko)
        {
            throw new NotImplementedException();
        }
    }
}