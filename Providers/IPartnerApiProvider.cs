using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;

namespace Brunda.Minimal.Providers
{
    internal interface IPartnerApiProvider
    {
        Task<SearchOfferResponse?> SearchOfferAsync(SearchOfferQueryParameters queryParameters, CancellationToken cancellationToken);
    }
}