using System.Net.Http.Headers;
using Wanderling.Application.Interfaces;

namespace Wanderling.Infrastructure.Services
{
    /// <summary>
    /// Provides functionality for identifying plants based on images by interacting with an external API.
    /// </summary>
    /// <remarks>This service is designed to facilitate plant identification by sending images to a
    /// third-party API.  It requires an API key and a valid API URL to function. The service is intended for scenarios
    /// where  plant recognition is needed, such as in gardening, agriculture, or educational applications.</remarks>
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

        /// <summary>
        /// Identifies a plant based on the provided image.
        /// </summary>
        /// <remarks>This method sends the provided image to an external API for plant identification. 
        /// Ensure that the image is in the correct format and that the API key is properly configured.</remarks>
        /// <param name="plantImage">A byte array representing the image of the plant to be identified. The image must be in JPEG format.</param>
        /// <returns>A JSON string containing the identification results, including details about the plant. The structure of the
        /// JSON depends on the API response.</returns>
        public async Task<string> IdentifyPlantAsync(byte[] plantImage)
        {
            using var content = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(plantImage);

            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            content.Add(imageContent, "images", "plant.jpg");

            _httpClient.DefaultRequestHeaders.Add("Api-Key", _apiKey);

            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
