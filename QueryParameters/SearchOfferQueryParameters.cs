using Refit;

namespace Brunda.Minimal.QueryParameters;

internal record SearchOfferQueryParameters
{
    [AliasAs("type")]
    public string ResidenceContractType { get; init; } = "koop";
    [AliasAs("zo")]
    public string? SearchQuery { get; init; }
    [AliasAs("page")]
    public int? Page { get; init; }
    [AliasAs("pagesize")]
    public int? PageSize { get; init; }
}
