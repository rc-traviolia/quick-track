using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.ClassEntityDataService
{
    public class DefaultClassEntityDataService : IClassEntityDataService
    {
        private readonly HttpClient _httpClient;

        public DefaultClassEntityDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClassEntityDto> AddClassEntity(ClassEntityForCreationDto classEntityForCreation)
        {
            var classEntityForCreationJson =
                new StringContent(JsonSerializer.Serialize(classEntityForCreation), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/classentities", classEntityForCreationJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ClassEntityDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteClassEntity(string loggedInUserName)
        {
            await _httpClient.DeleteAsync($"api/classentities/{loggedInUserName}");
        }

        public async Task<IEnumerable<ClassEntityWithoutChildrenDto>> GetAllClassEntities()
        {
                return await JsonSerializer.DeserializeAsync<IEnumerable<ClassEntityWithoutChildrenDto>>
                    (await _httpClient.GetStreamAsync($"api/classentities"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            
        }

        public async Task<ClassEntityDto> GetClassEntity(string loggedInUserName)
        {
            return await JsonSerializer.DeserializeAsync<ClassEntityDto>
                (await _httpClient.GetStreamAsync($"api/classentities/{loggedInUserName}?includeChildren=true"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ClassEntityWithoutChildrenDto> GetClassEntityWithoutChildren(string loggedInUserName)
        {
            return await JsonSerializer.DeserializeAsync<ClassEntityWithoutChildrenDto>
                (await _httpClient.GetStreamAsync($"api/classentities/{loggedInUserName}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public Task UpdateClassEntity(ClassEntityForUpdateDto updatedClassEntity)
        {
            //var employeeJson =
            //    new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            //await _httpClient.PutAsync("api/employee", employeeJson);
            throw new NotImplementedException();
        }
    }
}
