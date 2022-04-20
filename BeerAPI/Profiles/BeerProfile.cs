using System.Collections.Generic;

namespace BeerAPI.Profiles
{
    public class BeerProfile : AutoMapper.Profile
    {
        public BeerProfile()
        {
            CreateMap<Db.Beer, Models.Beer>();
            CreateMap<Models.Beer, Db.Beer>();
            
        }
    }
}
