using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickTrackWeb.Api.Repository;
using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Api.Controllers
{
    [Route("api/classentities")]
    public class ClassEntityController: Controller
    {
        private ILogger<ClassEntityController> _logger;
        private IQuickTrackApiRepository _repo;
        private IMapper _mapper;
        public ClassEntityController(
            ILogger<ClassEntityController> logger,
            IQuickTrackApiRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetClassEntities()
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var classEntities = _repo.GetAllClassEntities();

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var results = _mapper.Map<IEnumerable<ClassEntityWithoutChildrenDto>>(classEntities);


            return Ok(results);

        }
       
        [ActionName("GetClassEntityByOwnerIdentityName")]
        [HttpGet("{ownerIdentityName}")]
        public IActionResult GetClassEntityByOwnerIdentityName(string ownerIdentityName)
        {
            var classEntity = _repo.GetClassEntity(ownerIdentityName);

            if (classEntity == null)
            {
                return NotFound();
            }

            var classEntityResult = _mapper.Map<ClassEntityDto>(classEntity);
            return Ok(classEntityResult);


            //Example of OLD CODE
            ////find city
            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            //if(cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);
            ///* original way
            //return new JsonResult(
            //    CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id)
            //    ); */
        }

        [HttpPost]
        public IActionResult CreateClassEntity(
            [FromBody]  ClassEntityForCreationDto newClassEntityDto)
        {
            if (newClassEntityDto == null)
            {
                return BadRequest();
            }

            //if (newClassEntityDto.Name == "testforbadname")
            //{
            //    ModelState.AddModelError("Description", "The provided description should be different from the name.");
            //}

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            if (_repo.ClassEntityExists(newClassEntityDto.OwnerIdentityName))
            {
                return BadRequest("You already have a class.");
            }



            var finalClassEntity = _mapper.Map<Entities.ClassEntity>(newClassEntityDto);

            _repo.AddClassEntity(finalClassEntity);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a class for user {newClassEntityDto.OwnerIdentityName}.");
            }
            var createdClassEntityToReturn = _mapper.Map<ClassEntityDto>(finalClassEntity);

            return CreatedAtAction("GetClassEntityByOwnerIdentityName", new
            { ownerIdentityName = createdClassEntityToReturn.OwnerIdentityName}, createdClassEntityToReturn);



        }

        [HttpDelete("{loggedInUserName}")]
        public IActionResult DeleteEmployee(string loggedInUserName)
        {
            if (string.IsNullOrEmpty(loggedInUserName))
            {
                return BadRequest();
            }
                

            var classEntityToDelete = _repo.GetClassEntity(loggedInUserName);
            if (classEntityToDelete == null)
                return NotFound();

            _repo.DeleteClassEntity(loggedInUserName);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a class for user {loggedInUserName}.");
            }

            return NoContent();//success
        }
    }
}
