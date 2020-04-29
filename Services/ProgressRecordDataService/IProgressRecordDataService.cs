using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.ProgressRecordDataService
{
    public interface IProgressRecordDataService
    {
        Task<IEnumerable<ProgressRecordDto>> GetProgressRecordsForClassEntityAndWeek(string classEntityOwnerIdentityName, int weekId);
        Task<IEnumerable<ProgressRecordDto>> GetAllProgressForClassEntity(string classEntityOwnerIdentityName);
        Task<ProgressRecordDto> AddOrReplaceProgressRecord(ProgressRecordForCreationDto progressRecord);
        Task DeleteProgressRecord(int progressRecordId);
    }
}
