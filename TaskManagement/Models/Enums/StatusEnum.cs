using System.Text.Json.Serialization;

namespace TaskManagement.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Todo,
        InProgress,
        Done
    }
}
