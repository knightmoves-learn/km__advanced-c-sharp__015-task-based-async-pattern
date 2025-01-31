using System.Text.Json.Serialization;

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
}