using QuickTrackWeb.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.ClassEntityDataService
{
    public interface IClassEntityDataService
    {
        Task<IEnumerable<ClassEntityWithoutChildrenDto>> GetAllClassEntities();
        Task<ClassEntityDto> GetClassEntity(string loggedInUserName);
        Task<ClassEntityWithoutChildrenDto> GetClassEntityWithoutChildren(string loggedInUserName);
        Task<ClassEntityDto> AddClassEntity(ClassEntityForCreationDto newClassEntity);
        Task UpdateClassEntity(ClassEntityForUpdateDto updatedClassEntity);
        Task DeleteClassEntity(string loggedInUserName);
    }
}
