using Brunda.Minimal.Clients;
using Brunda.Minimal.Configuration;
using Brunda.Minimal.Constants;
using Brunda.Minimal.Providers;
using Brunda.Minimal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Refit;
using System.Threading.RateLimiting;

namespace Brunda.Minimal.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBrundaServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IRealEstateAgentService, RealEstateAgentService>()
            .AddScoped<IRealEstateAgentProvider, RealEstateAgentProvider>();
    }

    public static IServiceCollection AddPartnerApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddRefitClient<IPartnerApiClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var partnerApiSettings = configuration.Get<PartnerApiSettings>()
                    ?? throw new InvalidOperationException($"{nameof(PartnerApiSettings)} not configured properly");

                client.BaseAddress = partnerApiSettings.BaseAddress;
            })
            .AddStandardResilienceHandler()
            .Services;
    }

    public static IServiceCollection AddOptionsWithValidation<TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TOptions : class
    {
        return services
            .AddOptions<TOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart()
            .Services;
    }

    public static IServiceCollection AddResiliencePartnerApiPipeline(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddResiliencePipeline(ResiliencePipelineConstants.PartnerApiKey, (builder, context) =>
            {
                var resilienceOptions = configuration.GetRequiredSection(ConfigurationConstants.ResilienceOptionsSectionKey).Get<ResilienceOptionsSettings>()
                    ?? throw new InvalidOperationException($"{nameof(ResilienceOptionsSettings)} not configured properly");

                _ = builder
                    .AddRetry(new RetryStrategyOptions
                    {
                        BackoffType = DelayBackoffType.Exponential,
                        Delay = resilienceOptions.RetryOptions.Delay,
                        MaxRetryAttempts = resilienceOptions.RetryOptions.MaxRetryAttempts
                    })
                    .AddRateLimiter(new FixedWindowRateLimiter(
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = resilienceOptions.RateLimitOptions.PermitLimit,
                            Window = resilienceOptions.RateLimitOptions.LimitWindow,
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));
            });
    }
}
