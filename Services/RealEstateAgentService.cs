using Brunda.Minimal.Constants;
using Brunda.Minimal.Models;
using Brunda.Minimal.Providers;
using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;
using Microsoft.Extensions.Logging;

namespace Brunda.Minimal.Services;

internal class RealEstateAgentService(
    IRealEstateAgentProvider realEstateAgentProvider,
    ILogger<RealEstateAgentService> logger) : IRealEstateAgentService
{
    private readonly IRealEstateAgentProvider _realEstateAgentProvider = realEstateAgentProvider;
    private readonly ILogger<RealEstateAgentService> _logger = logger;

    public async Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetAsync(string searchQuery, CancellationToken cancellationToken)
    {
        var currentPage = 1;
        int pagesLeft;

        var realEstateAgents = new List<ResidenceResponse>();
        do
        {
            var queryParameters = new OfferQueryParameters
            {
                SearchQuery = searchQuery,
                Page = currentPage,
                PageSize = PartnerApiConstants.MaxPageSize,
            };

            var result = await _realEstateAgentProvider.GetSummaryDataAsync(queryParameters, cancellationToken).ConfigureAwait(false);
            if (result == null)
            {
                _logger.LogWarning("The partner API returned an invalid response for page {CurrentPage} that couldn't be resolved", currentPage);
                break;
            }

            realEstateAgents.AddRange(result.Residences);
            pagesLeft = result.Paging.TotalPages - result.Paging.CurrentPage;
            currentPage = result.Paging.CurrentPage + 1;
        } while (pagesLeft > 0);

        return realEstateAgents
            .GroupBy(x => x.RealEstateAgentId)
            .Select(x => new RealEstateAgentSummaryModel
            {
                ForSaleCount = x.Count(),
                RealEstateAgentId = x.Key,
                RealEstateAgentName = x.First().RealEstateAgentName
            })
            .ToList();
    }
}
