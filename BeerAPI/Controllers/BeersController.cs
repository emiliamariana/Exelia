using BeerAPI.Models;
using BeerAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BeerAPI.Controllers
{
    [Route("api/beers")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerRepository _beerRepository;

        public BeersController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Beer>> GetBeers()
        {
            return await _beerRepository.GetBeers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            return await _beerRepository.GetBeerById(id);
        }

        [HttpGet("match/{match}")]
        public async Task<IEnumerable<Beer>> GetMatchedBeers(string match)
        {
            return await _beerRepository.GetBeersByMatch(match);
        }

        [HttpPost]
        public async Task<ActionResult> AddBeer([FromBody] Beer beer)
        {
            Beer newBeer = await _beerRepository.Create(beer);
            if (newBeer == null)
            {
                return BadRequest("Beer already exists");
            }
            return CreatedAtAction(nameof(GetBeer), new { id = newBeer.Id }, newBeer);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBeer(int id, [FromBody] Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            await _beerRepository.Update(beer);

            return Ok(beer);
        }

        [HttpPut("rating")]
        public async Task<ActionResult> UpdateBeerRating(int id, int rating)
        {
            Beer beer = await _beerRepository.GetBeerById(id);
            if (beer != null)
            {
                await _beerRepository.UpdateRating(id, rating);
                return Ok(await _beerRepository.GetBeerById(id));
            }

            return BadRequest("Beer not found");

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBeer(int id)
        {
            Beer beer = await _beerRepository.GetBeerById(id);
            if (beer == null)
            {
                return NotFound("Beer not found");
            }
            await _beerRepository.Delete(id);
            return Ok();
        }
    }
}
