using Microsoft.EntityFrameworkCore;

namespace BeerAPI.Db
{
    public class BeerContext : DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Beer> Beers { get; set; } 
    }
}
