using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace E_Nompilo_Healthcare_system.Service
{
    public class MedicalService
    {

        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://healthcare.googleapis.com"; // Replace with the actual API URL

        public MedicalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string[]> GetConditionsBySymptoms(string[] symptoms)
        {
            // Create the API endpoint URL based on the symptoms provided
            string apiUrl = $"{ApiBaseUrl}/conditions?symptoms={string.Join(",", symptoms)}";

            // Send the HTTP GET request to the API and deserialize the response
            var response = await _httpClient.GetFromJsonAsync<string[]>(apiUrl);

            return response;
        }
    }
}

