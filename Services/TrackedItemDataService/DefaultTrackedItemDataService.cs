using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.TrackedItemDataService
{
    public class DefaultTrackedItemDataService : ITrackedItemDataService
    {
        public Task<StudentDto> AddTrackedItem(string classEntityOwnerIdentityName, TrackedItemForCreationDto trackedItemToAdd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTrackedItem(int trackedItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TrackedItemWithoutProgressDto>> GetTrackedItemsForClass(string classEntityOwnerIdentityName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTrackedItem()
        {
            throw new NotImplementedException();
        }
    }
}
