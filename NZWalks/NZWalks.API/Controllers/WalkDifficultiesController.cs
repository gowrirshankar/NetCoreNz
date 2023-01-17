using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficutyAsync()
        {
           var walkDifficulties = await walkDifficultyRepository.GetAllWalkDifficultyAsync();

            if(walkDifficulties == null)
                return NotFound();

            var walkDifficultiesDTO = mapper.Map<List<WalkDifficultyDTO>>(walkDifficulties);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficutyAsync")]
        public async Task<IActionResult> GetWalkDifficutyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyAsync(id);

            if(walkDifficulty == null)
                return NotFound();

            //Convert Domain to DTO's

            var walkDifficultDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);

            return Ok(walkDifficultDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Validate incoming request
            if(! ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to domain model
            var walkDifficultyDomain = new WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            //Call the repository
            walkDifficultyDomain = await walkDifficultyRepository.AddWalkDifficultyAsync(walkDifficultyDomain);

            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO
            var walkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficutyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulyAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Validate incoming request
            if(! ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to Domain model
            var walkDifficultyDomain = new WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            //Call Repository
            walkDifficultyDomain = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO
            var walkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficultyDomain);
            //Return response
            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulyAsync([FromRoute] Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);
            if(walkDifficulty == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            var walkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest), $"{nameof(addWalkDifficultyRequest)} Walk difficulty Request data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code), $"{nameof(addWalkDifficultyRequest.Code)} is required.");
            }            

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest), $"{nameof(updateWalkDifficultyRequest)} Walk difficulty Request data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code), $"{nameof(updateWalkDifficultyRequest.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
