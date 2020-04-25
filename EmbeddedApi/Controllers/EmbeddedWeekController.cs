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
    public class EmbeddedWeekController : Controller
    {

        private ILogger<EmbeddedWeekController> _logger;
        private IQuickTrackEmbeddedApiRepository _repo;
        private IMapper _mapper;
        public EmbeddedWeekController(
            ILogger<EmbeddedWeekController> logger,
            IQuickTrackEmbeddedApiRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet("{classEntityOwnerIdentityName}/weeks")]
        public IActionResult GetAllWeeksFromClassEntity(string classEntityOwnerIdentityName, bool includeChildren = false)
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var weeks = _repo.GetAllWeeksFromClass(classEntityOwnerIdentityName);

            if (weeks == null)
            {
                return NotFound();
            }

            if (includeChildren)
            {
                var results = _mapper.Map<IEnumerable<WeekDto>>(weeks);
                return Ok(results);
            }
            else
            {
                var results = _mapper.Map<IEnumerable<WeekWithoutProgressDto>>(weeks);
                return Ok(results);
            }


        }

        [ActionName("GetWeek")]
        [HttpGet("weeks/{weekId}")]
        public IActionResult GetWeek(int weekId)
        {
            var week = _repo.GetWeek(weekId);

            if (week == null)
            {
                return NotFound();
            }

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var results = _mapper.Map<WeekDto>(week);


            return Ok(results);

        }

        [HttpPost("{ownerIdentityName}/weeks")]
        public IActionResult CreateWeek(
            string ownerIdentityName,
            [FromBody]  WeekForCreationDto newWeekDto)
        {
            if (newWeekDto == null)
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

            var finalWeek = _mapper.Map<Entities.Week>(newWeekDto);

            //START: Might be able to replace this by defining student name as a Key value for EFCore
            var weeks = _repo.GetAllWeeksFromClass(ownerIdentityName);
            foreach (var s in weeks)
            {
                if (s.Number == finalWeek.Number)
                {
                    return UnprocessableEntity("There is already a Week with that number. How did that happen?");
                }
            }
            //STOP: Might be able to replace this by defining student name as a Key value for EFCore

            _repo.AddWeek(ownerIdentityName, finalWeek);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a week with number:  {newWeekDto.Number}.");
            }
            var weekToReturn = _mapper.Map<WeekDto>(finalWeek);

            return CreatedAtAction("GetWeek", new
            { weekId = weekToReturn.Id }, weekToReturn);



        }

        [HttpDelete("weeks/{weekId}")]
        public IActionResult DeleteWeek(int weekId)
        {
            if (!_repo.WeekExists(weekId))
            {
                return NotFound();
            }
            var weekToDelete = _repo.GetWeek(weekId);

            _repo.DeleteWeek(weekToDelete);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to delete a week with id: {weekId}.");
            }

            return NoContent();//success
        }

    }
}
