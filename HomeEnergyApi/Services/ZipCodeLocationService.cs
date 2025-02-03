using System.Text.Json.Serialization;
using HomeEnergyApi.Models;

namespace HomeEnergyApi.Services
{
    public class ZipLocationResponse
    {
        [JsonPropertyName("post code")]
        public required string PostCode { get; set; }
        public required string Country { get; set; }
        public required List<Place> Places { get; set; }
    }

    public class Place
    {
        [JsonPropertyName("place name")]
        public required string placename { get; set; }
        public required string State { get; set; }
    }

    public class ZipCodeLocationService
    {
        HttpClient httpClient;

        public ZipCodeLocationService(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("HomeEnergyApi/1.0");
            this.httpClient = httpClient;
        }

        public async Task<Place> Report(int zipCode)
        {
            var response = await httpClient.GetFromJsonAsync<ZipLocationResponse>($"https://api.zippopotam.us/us/{zipCode}");    
            return response.Places.FirstOrDefault();
        }
    }
}