using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;

namespace Brunda.Minimal.Providers
{
    internal interface IRealEstateAgentProvider
    {
        Task<OfferResponse?> GetSummaryDataAsync(OfferQueryParameters queryParameters, CancellationToken cancellationToken);
    }
}