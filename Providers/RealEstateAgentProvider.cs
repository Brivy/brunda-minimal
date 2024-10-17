using Brunda.Minimal.Clients;
using Brunda.Minimal.Configuration;
using Brunda.Minimal.Constants;
using Brunda.Minimal.QueryParameters;
using Brunda.Minimal.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Registry;
using Refit;

namespace Brunda.Minimal.Providers;

internal class RealEstateAgentProvider(
    IPartnerApiClient partnerApiClient,
    ResiliencePipelineProvider<string> resiliencePipelineProvider,
    IOptionsSnapshot<PartnerApiSettings> options,
    ILogger<RealEstateAgentProvider> logger) : IRealEstateAgentProvider
{
    private readonly IPartnerApiClient _partnerApiClient = partnerApiClient;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    private readonly PartnerApiSettings _settings = options.Value;
    private readonly ILogger<RealEstateAgentProvider> _logger = logger;

    public async Task<OfferResponse?> GetSummaryDataAsync(OfferQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        return await _resiliencePipelineProvider.GetPipeline(ResiliencePipelineConstants.PartnerApiKey)
            .ExecuteAsync(async (t, token) =>
            {
                _logger.LogInformation("Getting real estate agent data from partner API with query parameters: {QueryParameters}", queryParameters);

                try
                {
                    var result = await _partnerApiClient.GetOffersAsync(_settings.ApiKey, queryParameters, cancellationToken).ConfigureAwait(false);
                    if (!result.IsSuccessStatusCode || result.Content == null)
                    {
                        _logger.LogWarning("Received an invalid status code from the partner API: {StatusCode}", result.StatusCode);
                        return null;
                    }

                    return result.Content;
                }
                catch (ApiException ex)
                {
                    _logger.LogError(ex, "Failed to get real estate agent data from partner API with query parameters: {QueryParameters}", queryParameters);
                    return null;
                }
            }, cancellationToken).ConfigureAwait(false);
    }
}
