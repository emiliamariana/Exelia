using AutoMapper;
using BeerAPI.Db;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAPI.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerContext _context;
        private readonly IMapper _mapper;

        public BeerRepository(BeerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<Models.Beer> Create(Models.Beer beer)
        {
            Beer existentBeer = _context.Beers.Any() ? _context.Beers.Where(b => b.Type == beer.Type && b.Name == beer.Name).First() : null;
            if (existentBeer == null)
            {
                Beer newBeer = _mapper.Map<Models.Beer, Db.Beer>(beer);
                newBeer.RatingCounter = newBeer.Rating != null ? 1 : 0;

                _context.Beers.Add(newBeer);
                await _context.SaveChangesAsync();

                return _mapper.Map<Db.Beer, Models.Beer>(newBeer);
            }
            return null;
        }

        public async Task Delete(int id)
        {
            Beer beerToDelete = await _context.Beers.FindAsync(id);
            _context.Beers.Remove(beerToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.Beer> GetBeerById(int id)
        {
            Beer beer = await _context.Beers.FindAsync(id);
            return _mapper.Map<Db.Beer, Models.Beer>(beer);
        }

        public async Task<IEnumerable<Models.Beer>> GetBeers()
        {
            IEnumerable<Beer> beers = await _context.Beers.ToListAsync();
            return _mapper.Map<IEnumerable<Db.Beer>, IEnumerable<Models.Beer>>(beers);
        }

        public async Task<IEnumerable<Models.Beer>> GetBeersByMatch(string match)
        {
            IEnumerable<Beer> beers = await _context.Beers.Where(beer => beer.Name.ToLower().Contains(match.ToLower())).ToListAsync();
            return _mapper.Map<IEnumerable<Db.Beer>, IEnumerable<Models.Beer>>(beers);
        }

        public async Task Update(Models.Beer beer)
        {
            _context.Entry(_mapper.Map<Models.Beer, Db.Beer>(beer)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRating(int id, double rating)
        {
            Beer beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                beer.RatingCounter++;
                beer.Rating = (beer.Rating + rating) / beer.RatingCounter;
            }

            _context.Entry((beer)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
