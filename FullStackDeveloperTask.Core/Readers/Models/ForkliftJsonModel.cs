using System.Text.Json.Serialization;

namespace FullStackDeveloperTask.Core.Readers.Models
{
    public class ForkliftJsonModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("modelNumber")]
        public string ModelNumber { get; set; } = string.Empty;

        [JsonPropertyName("manufacturingDate")]
        public DateTime ManufacturingDate { get; set; }
    }
}
