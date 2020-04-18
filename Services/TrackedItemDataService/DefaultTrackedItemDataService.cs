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
  
        public async Task<StudentDto> AddTrackedItem(string classEntityOwnerIdentityName, TrackedItemForCreationDto trackedItemToAdd)
        {
            return null;
            throw new NotImplementedException();

            //var classEntityForCreationJson =
            //   new StringContent(JsonSerializer.Serialize(trackedItemToAdd), Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync($"api/classentities/{classEntityOwnerIdentityName}/students", classEntityForCreationJson);

            //if (response.IsSuccessStatusCode)
            //{
            //    return await JsonSerializer.DeserializeAsync<StudentDto>(await response.Content.ReadAsStreamAsync());
            //}

            //return null;
        }

        public async Task DeleteTrackedItem(int trackedItemId)
        {
            return;

            throw new NotImplementedException();

            //await _httpClient.DeleteAsync($"api/classentities/students/{studentId}");
        }

        public async Task<IEnumerable<TrackedItemWithoutProgressDto>> GetTrackedItemsForClass(string classEntityOwnerIdentityName)
        {
            return null;
            throw new NotImplementedException();
            //return await JsonSerializer.DeserializeAsync<IEnumerable<StudentWithoutProgressDto>>
            //        (await _httpClient.GetStreamAsync($"api/classentities/{classEntityOwnerIdentityName}/students"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public Task UpdateTrackedItem()
        {
            throw new NotImplementedException();
        }
    }
}
