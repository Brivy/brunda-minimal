using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;
using Refit;

namespace Brunda.Minimal.Clients;

internal interface IPartnerApiClient
{
    [Get("/{apiKey}")]
    Task<ApiResponse<SearchOfferResponse>> SearchOfferAsync(string apiKey, SearchOfferQueryParameters queryParameters, CancellationToken cancellationToken);
}
