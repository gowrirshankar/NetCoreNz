using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();

            //Return the data from the DTO(Data Transfer Object) instead of Domain(DB)
            //var regionsDTO = new List<RegionDTO>();

            //regions.ToList().ForEach(regionDB =>
            //{
            //    var regionDTO = new RegionDTO()
            //    {
            //        Id = regionDB.Id,
            //        Code = regionDB.Code,
            //        Name = regionDB.Name,
            //        Area = regionDB.Area,
            //        Lat = regionDB.Lat,
            //        Long = regionDB.Long,
            //        Population = regionDB.Population
            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            //Using AutoMapper

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions); // mapper.Map<List<Destination>>(Source);
            return Ok(regionsDTO);
        }
    }
}
