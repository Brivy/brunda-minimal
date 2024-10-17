using Brunda.Minimal.Configuration;
using Brunda.Minimal.Constants;
using Brunda.Minimal.Extensions;
using Brunda.Minimal.Models;
using Brunda.Minimal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
               .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var partnerApiConfiguration = configuration.GetRequiredSection(ConfigurationConstants.PartnerApiSectionKey);
        _ = services
            .AddBrundaServices()
            .AddOptionsWithValidation<PartnerApiSettings>(partnerApiConfiguration)
            .AddPartnerApiClient(partnerApiConfiguration)
            .AddResiliencePartnerApiPipeline(partnerApiConfiguration);
    })
    .Build();

await using (var serviceScope = host.Services.CreateAsyncScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var realEstateAgentService = serviceProvider.GetRequiredService<IRealEstateAgentService>();

    var realEstateAgents = await realEstateAgentService.GetDetailsAsync(SearchQueryConstants.AmsterdamSearchQuery, CancellationToken.None);
    var realEstateAgentsWithGarden = await realEstateAgentService.GetDetailsAsync(SearchQueryConstants.AmsterdamWithGardenSearchQuery, CancellationToken.None);

    Console.WriteLine();
    WriteResults(realEstateAgents);
    Console.WriteLine();
    WriteResults(realEstateAgentsWithGarden, true);

    static void WriteResults(IReadOnlyCollection<RealEstateAgentDetailsModel> realEstateAgents, bool withGarden = false)
    {
        var position = 1;
        var topTenAgents = realEstateAgents.ToTopTen();
        Console.WriteLine(!withGarden
            ? "TOP 10 REAL ESTATE AGENTS THAT SELL THE MOST PROPERTIES IN AMSTERDAM"
            : "TOP 10 REAL ESTATE AGENTS THAT SELL THE MOST PROPERTIES WITH A GARDEN IN AMSTERDAM");
        Console.WriteLine("---------------------------------------------------");
        foreach (var topTenAgent in topTenAgents)
        {
            Console.WriteLine($"{position++}: '{topTenAgent.RealEstateAgentName}' with {topTenAgent.ForSaleCount} properties for sale");
        }
    }
}

