namespace Brunda.Minimal.Models;

public record RealEstateAgentDetailsModel
{
    public required int RealEstateAgentId { get; init; }
    public required string RealEstateAgentName { get; init; }
    public required int ForSaleCount { get; init; }
}
