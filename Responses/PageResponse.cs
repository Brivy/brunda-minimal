using System.Text.Json.Serialization;

namespace Brunda.Minimal.Responses;

internal record PageResponse
{
    [JsonPropertyName("HuidigePagina")]
    public required int CurrentPage { get; init; }
    [JsonPropertyName("AantalPaginas")]
    public required int TotalPages { get; init; }
}
