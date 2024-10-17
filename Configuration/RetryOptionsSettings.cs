using System.ComponentModel.DataAnnotations;

namespace Brunda.Minimal.Configuration;

internal record RetryOptionsSettings
{
    [Required]
    public required TimeSpan Delay { get; init; }
    [Required]
    public required int MaxRetryAttempts { get; init; }
}
