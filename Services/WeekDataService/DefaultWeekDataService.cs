using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.WeekDataService
{
    public class DefaultWeekDataService : IWeekDataService
    {
        public Task<WeekDto> AddWeek(string classEntityOwnerIdentityName, WeekForCreationDto weekToAdd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWeek(int weekId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WeekWithoutProgressDto>> GetWeeksForClass(string classEntityOwnerIdentityName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWeek()
        {
            throw new NotImplementedException();
        }
    }
}
