using System.Net.Http.Headers;
using Wanderling.Application.Interfaces;

namespace Wanderling.Application.Services
{
    public class PlantRecognitionService : IPlantRecognitionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public PlantRecognitionService(HttpClient httpClient, string apiKey, string apiUrl)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _apiUrl = apiUrl;
        }

        public async Task<string> IdentifyPlantAsync(byte[] plantImage)
        {
            var requestContent = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(plantImage);

            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "images", "plant.jpg");

            _httpClient.DefaultRequestHeaders.Add("Api-Key", _apiKey);

            var response = await _httpClient.PostAsync(_apiUrl, requestContent);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
