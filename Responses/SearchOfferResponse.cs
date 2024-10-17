using System.Text.Json.Serialization;

namespace Brunda.Minimal.Responses;

internal record SearchOfferResponse
{
    [JsonPropertyName("Objects")]
    public required IReadOnlyCollection<PropertyResponse> Properties { get; init; } = [];
    [JsonPropertyName("Paging")]
    public required PageResponse Paging { get; init; }
}
