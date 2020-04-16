using QuickTrackWeb.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.StudentDataService
{
    public interface IStudentDataService
    {
        //Task<ClassEntityDto> GetClassEntity(string loggedInUserName);
        //Task<ClassEntityDto> AddClassEntity(ClassEntityForCreationDto newClassEntity);C:\Users\richard.traviolia\quick-track\Services\StudentDataService\IStudentDataService.cs
        //Task UpdateClassEntity(ClassEntityForUpdateDto updatedClassEntity);
        Task<IEnumerable<StudentWithoutRecordsDto>> GetStudentsForClass(string classEntityOwnerIdentityName);
        Task<StudentDto> AddStudent(string classEntityOwnerIdentityName, StudentForCreationDto studentToAdd);
        Task DeleteStudent(int studentId);
    }
}
