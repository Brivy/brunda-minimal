using Brunda.Minimal.Models;

namespace Brunda.Minimal.Services
{
    internal interface IRealEstateAgentService
    {
        Task<IReadOnlyCollection<RealEstateAgentDetailsModel>> GetDetailsAsync(string searchQuery, CancellationToken cancellationToken);
    }
}