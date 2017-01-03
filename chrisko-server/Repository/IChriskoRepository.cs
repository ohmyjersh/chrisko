using System.Threading.Tasks;
using ChrisKo.Models;

namespace ChrisKo.Repository
{
    public interface IChriskoRepository
    {
        Task<Chrisko> GetChriskoByIdAsync(string id);
        Task<Chrisko> GetChriskoByShortUrlAsync(string shortUrl);
        Task<Chrisko> GetChriskoByUrlAsync(string url);
        Task AddChriskoAsync(Chrisko chrisko);
        Task UpdateChriskoAsync(string id, Chrisko chrisko);
    }
}