using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.ProgressRecordDataService
{
    public class DefaultProgressRecordDataService : IProgressRecordDataService
    {
        private HttpClient _httpClient;
        public DefaultProgressRecordDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProgressRecordDto> AddOrReplaceProgressRecord(ProgressRecordForCreationDto progressRecord)
        {
            var progressRecordForCreation =
               new StringContent(JsonSerializer.Serialize(progressRecord), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/classentities/progressrecords", progressRecordForCreation);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ProgressRecordDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public Task DeleteProgressRecord(int progressRecordId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProgressRecordDto>> GetProgressRecordsForClassEntityAndWeek(string classEntityOwnerIdentityName, int weekId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<ProgressRecordDto>>
                    (await _httpClient.GetStreamAsync($"api/classentities/{classEntityOwnerIdentityName}/weeks/{weekId}/progressrecords"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
