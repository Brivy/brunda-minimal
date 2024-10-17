using Brunda.Minimal.Models;

namespace Brunda.Minimal.Services
{
    internal interface IRealEstateAgentService
    {
        Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetAsync(string searchQuery, CancellationToken cancellationToken);
    }
}