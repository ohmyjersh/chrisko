using System;
using System.Threading.Tasks;
using ChrisKo.Config;
using ChrisKo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChrisKo.Repository
{
    public class ChriskoRepository : IChriskoRepository
    {
        private readonly ChriskoContext _context = null;
        public ChriskoRepository(IOptions<Settings> settings) {
            _context = new ChriskoContext(settings);
        }
        public async Task AddChriskoAsync(Chrisko chrisko)
        {
            await _context.Chrisko.InsertOneAsync(chrisko);
        }

        public async Task<Chrisko> GetChriskoByIdAsync(string id)
        {
            var filter = Builders<Chrisko>.Filter.Eq("Id", id);
            return await _context.Chrisko
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Chrisko> GetChriskoByUrlAsync(string url)
        {
            var filter = Builders<Chrisko>.Filter.Eq("Url", url);
            return await _context.Chrisko
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Chrisko> GetChriskoByShortUrlAsync(string shortUrl)
        {
            var filter = Builders<Chrisko>.Filter.Eq("shortUrl", shortUrl);
            return await _context.Chrisko
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task UpdateChriskoAsync(string id, Chrisko chrisko)
        {
            var filter = Builders<Chrisko>.Filter.Eq(s => s.Id, id);
            var update = Builders<Chrisko>.Update
                                .Set(s => s.Visits, chrisko.Visits)
                                .CurrentDate(s => s.UpdatedOn);
            await _context.Chrisko.UpdateOneAsync(filter, update);
        }
    }
}