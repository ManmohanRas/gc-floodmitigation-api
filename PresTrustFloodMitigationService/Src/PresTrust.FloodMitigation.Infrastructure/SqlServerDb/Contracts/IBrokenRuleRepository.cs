using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IBrokenRuleRepository
{
    /// <summary>
    /// get broken rules
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<List<FloodBrokenRuleEntity>> GetBrokenRulesAsync(int applicationId);
    /// <summary>
    /// save broken rule
    /// </summary>
    /// <param name="brokenRule"></param>
    /// <returns></returns>
    Task SaveBrokenRule(FloodBrokenRuleEntity brokenRule);
    /// <summary>
    /// save broken rules
    /// </summary>
    /// <param name="brokenRules"></param>
    /// <returns></returns>
    Task SaveBrokenRules(List<FloodBrokenRuleEntity> brokenRules);
    /// <summary>
    /// Delete broken rules 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    Task DeleteBrokenRulesAsync(int applicationId, ApplicationSectionEnum section);
    /// <summary>
    /// Delete all broken rules 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task DeleteAllBrokenRulesAsync(int applicationId);
}
