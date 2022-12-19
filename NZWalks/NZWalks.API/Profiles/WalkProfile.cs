using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile()
        {
            CreateMap<Walk, WalkDTO>().ReverseMap(); //Source(Walk), Destination(WalkDTO)
            CreateMap<WalkDifficulty, WalkDifficultyDTO>().ReverseMap(); //Source(WalkDifficulty), Destination(WalkDifficultyDTO)
        }
    }
}
