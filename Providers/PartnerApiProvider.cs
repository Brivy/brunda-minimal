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

internal class PartnerApiProvider(
    IPartnerApiClient partnerApiClient,
    ResiliencePipelineProvider<string> resiliencePipelineProvider,
    IOptionsSnapshot<PartnerApiSettings> options,
    ILogger<PartnerApiProvider> logger) : IPartnerApiProvider
{
    private readonly IPartnerApiClient _partnerApiClient = partnerApiClient;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    private readonly PartnerApiSettings _settings = options.Value;
    private readonly ILogger<PartnerApiProvider> _logger = logger;

    public async Task<SearchOfferResponse?> SearchOfferAsync(SearchOfferQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        return await _resiliencePipelineProvider.GetPipeline(ResiliencePipelineConstants.PartnerApiKey)
            .ExecuteAsync(async (t, token) =>
            {
                _logger.LogInformation("Getting data from partner API with query parameters: {QueryParameters}", queryParameters);

                try
                {
                    var result = await _partnerApiClient.SearchOfferAsync(_settings.ApiKey, queryParameters, cancellationToken).ConfigureAwait(false);
                    if (!result.IsSuccessStatusCode || result.Content == null)
                    {
                        _logger.LogWarning("Received an invalid status code from the partner API: {StatusCode}", result.StatusCode);
                        return null;
                    }

                    return result.Content;
                }
                catch (ApiException ex)
                {
                    _logger.LogError(ex, "Failed to get data from the partner API");
                    return null;
                }
            }, cancellationToken).ConfigureAwait(false);
    }
}
