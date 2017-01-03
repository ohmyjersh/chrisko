using System.Collections.Generic;
using System.Threading.Tasks;
using ChrisKo.Models;

namespace ChrisKo.Repository
{
    public interface IChriskoRepository
    {
        Task<Chrisko> GetChriskoByUrlId(string urlId);
        Task<Chrisko> GetChriskoByUrl(string url);
        Task AddChrisko(Chrisko chrisko);
        Task UpdateNote(string id, Chrisko chrisko);
    }
}