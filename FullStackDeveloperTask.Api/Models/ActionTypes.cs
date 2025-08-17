using System.Text.Json.Serialization;

namespace FullStackDeveloperTask.Api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActionTypes
    {
        Forward,
        Backward,
        Left,
        Right
    }
}
