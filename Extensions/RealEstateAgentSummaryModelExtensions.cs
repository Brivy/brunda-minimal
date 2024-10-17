using Brunda.Minimal.Models;

namespace Brunda.Minimal.Extensions;

internal static class RealEstateAgentSummaryModelExtensions
{
    public static IReadOnlyCollection<RealEstateAgentSummaryModel> ToTopTen(this IReadOnlyCollection<RealEstateAgentSummaryModel> realEstateAgentSummaries) =>
        realEstateAgentSummaries
            .OrderByDescending(x => x.ForSaleCount)
            .Take(10)
            .ToList();
}
