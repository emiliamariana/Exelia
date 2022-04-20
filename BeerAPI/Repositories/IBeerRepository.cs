using BeerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerAPI.Repositories
{
    public interface IBeerRepository
    {
        Task<IEnumerable<Beer>> GetBeers();
        Task<Beer> GetBeerById(int id);
        Task<Beer> Create(Beer beer);
        Task<IEnumerable<Beer>> GetBeersByMatch(string match);
        Task Update(Beer beer);
        Task UpdateRating(int id, double rating);
        Task Delete(int id);
    }
}
