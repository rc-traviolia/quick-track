using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.WeekDataService
{
    public class DefaultWeekDataService : IWeekDataService
    {
        private readonly HttpClient _httpClient;

        public DefaultWeekDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeekDto> AddWeek(string classEntityOwnerIdentityName, WeekForCreationDto weekToAdd)
        {
            var weekForCreation =
               new StringContent(JsonSerializer.Serialize(weekToAdd), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/classentities/{classEntityOwnerIdentityName}/weeks", weekForCreation);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<WeekDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public Task DeleteWeek(int weekId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WeekWithoutProgressDto>> GetWeeksForClass(string classEntityOwnerIdentityName)
        {
            var response = (await _httpClient.GetStreamAsync($"api/classentities/{classEntityOwnerIdentityName}/weeks"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            Console.WriteLine(response.ToString());

            return await JsonSerializer.DeserializeAsync<IEnumerable<WeekWithoutProgressDto>>
                     (await _httpClient.GetStreamAsync($"api/classentities/{classEntityOwnerIdentityName}/weeks"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public Task UpdateWeek()
        {
            throw new NotImplementedException();
        }
    }
}
