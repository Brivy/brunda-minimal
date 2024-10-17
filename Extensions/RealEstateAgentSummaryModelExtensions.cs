using Brunda.Minimal.Models;

namespace Brunda.Minimal.Extensions;

internal static class RealEstateAgentSummaryModelExtensions
{
    public static IReadOnlyCollection<RealEstateAgentDetailsModel> ToTopTen(this IReadOnlyCollection<RealEstateAgentDetailsModel> realEstateAgentSummaries) =>
        realEstateAgentSummaries
            .OrderByDescending(x => x.ForSaleCount)
            .Take(10)
            .ToList();
}
