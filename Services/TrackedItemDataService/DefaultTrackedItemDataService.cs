using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.TrackedItemDataService
{
    public class DefaultTrackedItemDataService : ITrackedItemDataService
    {
        private readonly HttpClient _httpClient;

        public DefaultTrackedItemDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
  
        public async Task<TrackedItemDto> AddTrackedItem(string classEntityOwnerIdentityName, TrackedItemForCreationDto trackedItemToAdd)
        {
            var trackedItemForCreation =
               new StringContent(JsonSerializer.Serialize(trackedItemToAdd), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/classentities/{classEntityOwnerIdentityName}/trackeditems", trackedItemForCreation);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<TrackedItemDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteTrackedItem(int trackedItemId)
        {
            await _httpClient.DeleteAsync($"api/classentities/trackeditems/{trackedItemId}");
        }

        public async Task<IEnumerable<TrackedItemWithoutProgressDto>> GetTrackedItemsForClass(string classEntityOwnerIdentityName)
        {
           return await JsonSerializer.DeserializeAsync<IEnumerable<TrackedItemWithoutProgressDto>>
                    (await _httpClient.GetStreamAsync($"api/classentities/{classEntityOwnerIdentityName}/trackeditems"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public Task UpdateTrackedItem()
        {
            throw new NotImplementedException();
        }
    }
}
