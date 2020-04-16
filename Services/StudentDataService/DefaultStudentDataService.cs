using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.StudentDataService
{
    public class DefaultStudentDataService : IStudentDataService
    {
        private readonly HttpClient _httpClient;

        public DefaultStudentDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<StudentDto> AddStudent(string classEntityOwnerIdentityName, StudentForCreationDto studentToAdd)
        {
            var classEntityForCreationJson =
               new StringContent(JsonSerializer.Serialize(studentToAdd), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/classentities/{classEntityOwnerIdentityName}/students", classEntityForCreationJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<StudentDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
        public async Task DeleteStudent(int studentId)
        {
            await _httpClient.DeleteAsync($"api/classentities/students/{studentId}");
        }
        public async Task<IEnumerable<StudentWithoutRecordsDto>> GetStudentsForClass(string classEntityOwnerIdentityName)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<StudentWithoutRecordsDto>>
                    (await _httpClient.GetStreamAsync($"api/classentities/{classEntityOwnerIdentityName}/students"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
