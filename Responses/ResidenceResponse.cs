using System.Text.Json.Serialization;

namespace Brunda.Minimal.Responses;

internal record ResidenceResponse
{
    [JsonPropertyName("MakelaarId")]
    public required int RealEstateAgentId { get; init; }
    [JsonPropertyName("MakelaarNaam")]
    public required string RealEstateAgentName { get; init; }
}
