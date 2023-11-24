namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public interface IPropertyBrokenRuleRepository
{
    /// <summary>
    /// get broken rules
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    //Task<List<FloodPropertyBrokenRuleEntity>> GetPropertyBrokenRulesAsync(int applicationId, string pamsPin);
    Task<List<FloodPropertyBrokenRuleEntity>> GetPropertyBrokenRulesAsync(int applicationId, string pamsPin);
    /// <summary>
    /// save broken rule
    /// </summary>
    /// <param name="brokenRule"></param>
    /// <returns></returns>
    Task SavePropertyBrokenRule(FloodPropertyBrokenRuleEntity brokenRule);
    /// <summary>
    /// save broken rules
    /// </summary>
    /// <param name="brokenRules"></param>
    /// <returns></returns>
    Task SavePropertyBrokenRules(List<FloodPropertyBrokenRuleEntity> brokenRules);
    /// <summary>
    /// Delete broken rules 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    Task DeletePropertyBrokenRulesAsync(int applicationId, PropertySectionEnum section, string pamsPin);
}
