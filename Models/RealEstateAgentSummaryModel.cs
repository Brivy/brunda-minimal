﻿namespace Brunda.Minimal.Models;

public record RealEstateAgentSummaryModel
{
    public required int RealEstateAgentId { get; init; }
    public required string RealEstateAgentName { get; init; }
    public required int ForSaleCount { get; init; }
}
