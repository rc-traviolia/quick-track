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
    public class EmbeddedProgressRecordController : Controller
    {

        private ILogger<EmbeddedProgressRecordController> _logger;
        private IQuickTrackEmbeddedApiRepository _repo;
        private IMapper _mapper;
        public EmbeddedProgressRecordController(
            ILogger<EmbeddedProgressRecordController> logger,
            IQuickTrackEmbeddedApiRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;  
        }

        [ActionName("GetProgressRecord")]
        [HttpGet("progressrecords/{progressRecordId}")]
        public IActionResult GetProgressRecord(int progressRecordId)
        {
            var progressRecord = _repo.GetProgressRecord(progressRecordId);

            if (progressRecord == null)
            {
                return NotFound();
            }

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var results = _mapper.Map<ProgressRecordDto>(progressRecord);


            return Ok(results);

        }

        [HttpGet("{classEntityOwnerIdentityName}/weeks/{weekId}/progressrecords")]
        public IActionResult GetProgressRecordsForClassEntityAndWeek(string classEntityOwnerIdentityName, int weekId)
        {
            if (!_repo.ClassEntityExists(classEntityOwnerIdentityName) ||
                !_repo.WeekExists(weekId))
            {
                return NotFound();
            }

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var progressRecordsToReturn = _repo.GetAllProgressRecordsFromClassAndWeek(classEntityOwnerIdentityName, weekId);
            var results = _mapper.Map<IEnumerable<ProgressRecordDto>>(progressRecordsToReturn);


            return Ok(results);

        }

        [HttpPost("progressrecords")]
        public IActionResult CreateReplaceProgressRecord(
            [FromBody]  ProgressRecordForCreationDto newProgressRecord)
        {
            if (newProgressRecord == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (!_repo.StudentExists(newProgressRecord.StudentId) ||
                !_repo.WeekExists(newProgressRecord.WeekId) ||
                !_repo.TrackedItemExists(newProgressRecord.TrackedItemId))
            {
                return NotFound();
            }

            var finalTrackedItem = _mapper.Map<Entities.ProgressRecord>(newProgressRecord);

            //START: DUPLICATE ITEM CHECK
            var existingProgressRecord = _repo.GetProgressRecordByForeignKeys(
                finalTrackedItem.ClassEntityId,
                finalTrackedItem.StudentId,
                finalTrackedItem.TrackedItemId,
                finalTrackedItem.WeekId);
            if (existingProgressRecord != null)
            {
                _repo.DeleteProgressRecord(existingProgressRecord);
                //return UnprocessableEntity("There is already a Progress Record with those Id's");
            }
            //END: DUPLICATE ITEM CHECK


            _repo.AddProgressRecord(finalTrackedItem);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a progress record with these Foreign Keys:" +
                    $"\nClassEntityId:{newProgressRecord.ClassEntityId}" +
                    $"\nStudentId:{newProgressRecord.StudentId}" +
                    $"\nTrackedItemId:{newProgressRecord.TrackedItemId}" +
                    $"\nWeekId:{newProgressRecord.WeekId}");

            }
            var progressRecordToReturn = _mapper.Map<ProgressRecordDto>(finalTrackedItem);

            return CreatedAtAction("GetProgressRecord", new
            { progressRecordId = progressRecordToReturn.Id }, progressRecordToReturn);



        }

        [HttpDelete("progressrecords/{progressRecordId}")]
        public IActionResult DeleteProgressRecord(int progressRecordId)
        {
            if (!_repo.ProgressRecordExists(progressRecordId))
            {
                return NotFound();
            }
            var progressToDelete = _repo.GetProgressRecord(progressRecordId);

            _repo.DeleteProgressRecord(progressToDelete);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to delete Progress Record with id: {progressRecordId}.");
            }

            return NoContent();//success
        }

    }
}
