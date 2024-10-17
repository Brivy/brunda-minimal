﻿using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Brunda.Minimal.Configuration;

internal record PartnerApiSettings
{
    [Required]
    public required Uri BaseAddress { get; init; }
    [Required]
    public required string ApiKey { get; init; }
    [Required]
    [ValidateObjectMembers]
    public required ResilienceOptionsSettings ResilienceOptions { get; init; }
}
