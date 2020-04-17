using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.TrackedItemDataService
{
    public interface ITrackedItemDataService
    {
        Task<IEnumerable<TrackedItemWithoutProgressDto>> GetTrackedItemsForClass(string classEntityOwnerIdentityName);
        Task<StudentDto> AddTrackedItem(string classEntityOwnerIdentityName, TrackedItemForCreationDto trackedItemToAdd);
        Task UpdateTrackedItem();
        Task DeleteTrackedItem(int trackedItemId);
    }
}
