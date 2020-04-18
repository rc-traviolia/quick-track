using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickTrackWeb.EmbeddedApi.Repository;
using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.EmbeddedApi.Controllers
{
    [Route("api/classentities")]
    public class EmbeddedTrackedItemController : Controller
    {

        private ILogger<EmbeddedTrackedItemController> _logger;
        private IQuickTrackEmbeddedApiRepository _repo;
        private IMapper _mapper;
        public EmbeddedTrackedItemController(
            ILogger<EmbeddedTrackedItemController> logger,
            IQuickTrackEmbeddedApiRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet("{classEntityOwnerIdentityName}/trackeditems")]
        public IActionResult GetAllTrackedItemsFromClassEntity(string classEntityOwnerIdentityName, bool includeChildern = false)
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var trackedItems = _repo.GetAllTrackedItemsFromClass(classEntityOwnerIdentityName);

            if (trackedItems == null)
            {
                return NotFound();
            }

            if (includeChildern)
            {
                var results = _mapper.Map<IEnumerable<TrackedItemDto>>(trackedItems);
                return Ok(results);
            }
            else
            {
                var results = _mapper.Map<IEnumerable<TrackedItemWithoutProgressDto>>(trackedItems);
                return Ok(results);
            }


        }

        [ActionName("GetTrackedItem")]
        [HttpGet("trackeditems/{trackedItemId}")]
        public IActionResult GetTrackedItem(int trackedItemId)
        {
            var trackedItem = _repo.GetTrackedItem(trackedItemId);

            if (trackedItem == null)
            {
                return NotFound();
            }

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var results = _mapper.Map<TrackedItemDto>(trackedItem);


            return Ok(results);

        }

        [HttpPost("{ownerIdentityName}/trackeditems")]
        public IActionResult CreateTrackedItem(
            string ownerIdentityName,
            [FromBody]  TrackedItemForCreationDto newTrackedItemDto)
        {
            if (newTrackedItemDto == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (!_repo.ClassEntityExists(ownerIdentityName))
            {
                return NotFound();
            }

            var finalTrackedItem = _mapper.Map<Entities.TrackedItem>(newTrackedItemDto);

            //START: Might be able to replace this by defining student name as a Key value for EFCore
            var trackedItems = _repo.GetAllTrackedItemsFromClass(ownerIdentityName);
            foreach (var s in trackedItems)
            {
                if (s.Name == finalTrackedItem.Name)
                {
                    return UnprocessableEntity("There is already a Tracked Item with that name in this Class");
                }
            }
            //STOP: Might be able to replace this by defining student name as a Key value for EFCore

            _repo.AddTrackedItem(ownerIdentityName, finalTrackedItem);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a Tracked Item with name:  {newTrackedItemDto.Name}.");
            }
            var trackedItemToReturn = _mapper.Map<TrackedItemDto>(finalTrackedItem);

            return CreatedAtAction("GetTrackedItem", new
            { trackedItemId = trackedItemToReturn.Id }, trackedItemToReturn);



        }

        [HttpDelete("trackeditems/{trackedItemId}")]
        public IActionResult DeleteTrackedItem(int trackedItemId)
        {
            if (!_repo.TrackedItemExists(trackedItemId))
            {
                return NotFound();
            }
            var trackedItemToDelete = _repo.GetTrackedItem(trackedItemId);

            _repo.DeleteTrackedItem(trackedItemToDelete);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to delete a TrackedItem with id: {trackedItemId}.");
            }

            return NoContent();//success
        }
    }
}
