using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Brunda.Minimal.Configuration;

internal record ResilienceOptionsSettings
{
    [Required]
    [ValidateObjectMembers]
    public required RateLimitOptionsSettings RateLimitOptions { get; init; }
    [Required]
    [ValidateObjectMembers]
    public required RetryOptionsSettings RetryOptions { get; init; }
}
