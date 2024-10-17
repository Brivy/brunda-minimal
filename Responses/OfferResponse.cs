using System.Text.Json.Serialization;

namespace Brunda.Minimal.Responses;

internal record OfferResponse
{
    [JsonPropertyName("Objects")]
    public required IReadOnlyCollection<ResidenceResponse> Residences { get; init; } = [];
    [JsonPropertyName("Paging")]
    public required PageResponse Paging { get; init; }
}
