using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            //Fetch data from database - domain walks
            var walks = await walkRepository.GetAllWalkAsync();

            //Return the data from the DTO(Data Transfer Object) instead of Domain(DB)
            //var walksDTO = new List<WalkDTO>();
            //walks.ToList().ForEach(walkDB =>
            //    {
            //        var walkDTO = new WalkDTO()
            //        {
            //            Id = walkDB.Id,
            //            Length = walkDB.Length,
            //            Name = walkDB.Name,
            //            WalkDifficultyId = walkDB.WalkDifficultyId,
            //            RegionId = walkDB.RegionId,
            //            Region = walkDB.RegionDTO,
            //            WalkDifficulty = walkDB.WalkDifficultyDTO
            //        };
            //        walksDTO.Add(walkDTO);
            //    });

            //Convert domain walks into DTO walks
            var walksDTO = mapper.Map<List<WalkDTO>>(walks);
            //Return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
           //Get walk domain object from database
           var walkDomain = await walkRepository.GetWalkByIdAsync(id);

           //Convert Domain object to DTO

           var walkDTO = mapper.Map<WalkDTO>(walkDomain);

           //Return response
           return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            // Convert incoming data(addWalkRequest) to Domain Model
            var walkDomain = new Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            //Pass domain object to repository to persist this
            walkDomain = await walkRepository.AddWalkAsync(walkDomain);

            //Converting domain object back to DTO
            var walkDTO = new WalkDTO
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //Send DTO response back to the client
            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkDTO.Id }, walkDTO);//Passing the action name, id value and the object to the CreatedAtAction method.
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            //Convert DTO to Domain Object
            var walkDomain = new Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
                RegionId = updateWalkRequest.RegionId
            };

            //Pass details to repository - Get Domain object in response (or null)
            walkDomain = await walkRepository.UpdateWalkAsync(id, walkDomain);

            //Handle Null (Not Found)
            if(walkDomain == null)
            {
                return NotFound();
            }

            //Convert back Domain to DTO
            var walkDTO = new WalkDTO
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
                RegionId = walkDomain.RegionId
            };

            //Return Response
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Call repository to delete the walk
            var walkDomain = await walkRepository.DeleteWalkAsync(id);
            
            if(walkDomain == null)
            {
                return NotFound();
            }

           var walkDTO =  mapper.Map<WalkDTO>(walkDomain);

            return Ok(walkDTO);
        }
    }
}

