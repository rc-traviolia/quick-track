using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.WeekDataService
{
    public interface IWeekDataService
    {
        Task<IEnumerable<WeekWithoutProgressDto>> GetWeeksForClass(string classEntityOwnerIdentityName);
        Task<WeekDto> AddWeek(string classEntityOwnerIdentityName, WeekForCreationDto weekToAdd);
        Task UpdateWeek();
        Task DeleteWeek(int weekId);
    }
}
