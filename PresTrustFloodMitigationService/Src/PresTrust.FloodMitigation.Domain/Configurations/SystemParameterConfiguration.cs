namespace PresTrust.FloodMitigation.Domain.Configurations;

public class SystemParameterConfiguration
{
    public int SQLCommandTimeoutInSeconds { get; set; }
    public int SqlConnectionRetryCount { get; set; }
    public int SqlConnectionRetryIntervalInSeconds { get; set; }
    public int BulkCopyTimeOutInSeconds { get; set; }
    public int BulkCopyBatchSize { get; set; }
    public int TransScopeTimeOutInMinutes { get; set; }
    public bool IsDevelopment { get; set; }
    public string TestEmailIds { get; set; }
    public string TestEmailFromUserName { get; set; }
    public int PollyRetryCount { get; set; }
    public int PollyRetryIntervalInSeconds { get; set; }
    public string PresTrustWebClient { get; set; }
    public string IdentityApiSubDomain { get; set; }
    public string SpacialApiSubDomain { get; set; }
    public string EmailApiSubDomain { get; set; }
    public int CutOffYearsForReInitiate { get; set; }
    public string ProgramAdminName { get; set; }
    public string ProgramAdminEmail { get; set; }
    public string? CC { get; set; }
}
