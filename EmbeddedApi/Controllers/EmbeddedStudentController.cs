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
    public class EmbeddedStudentController : Controller
    {
        private ILogger<EmbeddedStudentController> _logger;
        private IQuickTrackEmbeddedApiRepository _repo;
        private IMapper _mapper;
        public EmbeddedStudentController(
            ILogger<EmbeddedStudentController> logger,
            IQuickTrackEmbeddedApiRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{classEntityOwnerIdentityName}/students")]
        public IActionResult GetAllStudentsFromClassEntity(string classEntityOwnerIdentityName, bool includeChildern = false)
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var students = _repo.GetAllStudentsFromClass(classEntityOwnerIdentityName);

            if(students == null)
            {
                return NotFound();
            }

            if (includeChildern)
            {
                var results = _mapper.Map<IEnumerable<StudentDto>>(students);
                return Ok(results);
            }
            else
            {
                var results = _mapper.Map<IEnumerable<StudentWithoutProgressDto>>(students);
                return Ok(results);
            }
            

        }

        [ActionName("GetStudent")]
        [HttpGet("students/{studentId}")]
        public IActionResult GetStudent(int studentId)
        {
            var student = _repo.GetStudent(studentId);

            if (student == null)
            {
                return NotFound();
            }

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var results = _mapper.Map<StudentDto>(student);


            return Ok(results);

        }

        [HttpPost("{ownerIdentityName}/students")]
        public IActionResult CreateStudent(
            string ownerIdentityName,
            [FromBody]  StudentForCreationDto newStudentDto)
        {
            if (newStudentDto == null)
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

            var finalStudent = _mapper.Map<Entities.Student>(newStudentDto);

            //START: Might be able to replace this by defining student name as a Key value for EFCore
            var classStudents = _repo.GetAllStudentsFromClass(ownerIdentityName);
            foreach (var s in classStudents)
            {
                if (s.Name == finalStudent.Name)
                {
                    return UnprocessableEntity("There is already a Student with that name in this Class");
                }
            }
            //STOP: Might be able to replace this by defining student name as a Key value for EFCore

            _repo.AddStudent(ownerIdentityName, finalStudent);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a student with name:  {newStudentDto.Name}.");
            }
            var createdStudentToReturn = _mapper.Map<StudentDto>(finalStudent);

            return CreatedAtAction("GetStudent", new
            { studentId = createdStudentToReturn.Id }, createdStudentToReturn);



        }

        [HttpDelete("students/{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_repo.StudentExists(studentId))
            {
                return NotFound();
            }   
            var studentToDelete = _repo.GetStudent(studentId);

            _repo.DeleteStudent(studentToDelete);
            if (!_repo.Save())
            {
                return StatusCode(500, $"A problem happened while handling your request to create a Student with id: {studentId}.");
            }

            return NoContent();//success
        }
    }
}
