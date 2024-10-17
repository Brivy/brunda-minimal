using Brunda.Minimal.Constants;
using Brunda.Minimal.Models;
using Brunda.Minimal.Providers;
using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;
using Microsoft.Extensions.Logging;

namespace Brunda.Minimal.Services;

internal class RealEstateAgentService(
    IPartnerApiProvider realEstateAgentProvider,
    ILogger<RealEstateAgentService> logger) : IRealEstateAgentService
{
    private readonly IPartnerApiProvider _realEstateAgentProvider = realEstateAgentProvider;
    private readonly ILogger<RealEstateAgentService> _logger = logger;

    public async Task<IReadOnlyCollection<RealEstateAgentDetailsModel>> GetDetailsAsync(string searchQuery, CancellationToken cancellationToken)
    {
        var currentPage = 1;
        int pagesLeft;

        var properties = new List<PropertyResponse>();
        do
        {
            var queryParameters = new SearchOfferQueryParameters
            {
                SearchQuery = searchQuery,
                Page = currentPage,
                PageSize = PartnerApiConstants.MaxPageSize,
            };

            var searchOffer = await _realEstateAgentProvider.SearchOfferAsync(queryParameters, cancellationToken).ConfigureAwait(false);
            if (searchOffer == null)
            {
                _logger.LogWarning("The partner API returned an invalid response for page {CurrentPage} that couldn't be resolved", currentPage);
                break;
            }

            properties.AddRange(searchOffer.Properties);
            pagesLeft = searchOffer.Paging.TotalPages - searchOffer.Paging.CurrentPage;
            currentPage = searchOffer.Paging.CurrentPage + 1;
        } while (pagesLeft > 0);

        return properties
            .GroupBy(x => x.RealEstateAgentId)
            .Select(x => new RealEstateAgentDetailsModel
            {
                ForSaleCount = x.Count(),
                RealEstateAgentId = x.Key,
                RealEstateAgentName = x.First().RealEstateAgentName
            })
            .ToList();
    }
}
