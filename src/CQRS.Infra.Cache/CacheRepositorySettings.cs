namespace CQRS.Infra.Cache;

public class CacheRepositorySettings
{
    public string? ConnectionString { get; set; }
    public int TimeToLiveInSeconds { get; set; }
}