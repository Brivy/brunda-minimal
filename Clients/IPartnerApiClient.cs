using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;
using Refit;

namespace Brunda.Minimal.Clients;

internal interface IPartnerApiClient
{
    [Get("/{apiKey}")]
    Task<ApiResponse<OfferResponse>> GetOffersAsync(string apiKey, OfferQueryParameters queryParameters, CancellationToken cancellationToken);
}
