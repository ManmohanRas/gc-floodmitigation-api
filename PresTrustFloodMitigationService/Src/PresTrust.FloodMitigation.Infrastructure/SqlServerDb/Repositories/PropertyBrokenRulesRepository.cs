using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories
{
    public class PropertyBrokenRulesRepository : IPropertyBrokenRuleRepository
    {
        #region " Members ... "

        private readonly PresTrustSqlDbContext context;
        protected readonly SystemParameterConfiguration systemParamConfig;

        #endregion

        #region " ctor ... "

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="systemParamConfigOptions"></param>
        public PropertyBrokenRulesRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
        {
            this.context = context;
            systemParamConfig = systemParamConfigOptions.Value;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public async Task<List<FloodPropertyBrokenRuleEntity>> GetPropertyBrokenRulesAsync(int applicationId, string pamsPin)
        {
            List<FloodPropertyBrokenRuleEntity> results = default;

            using var conn = context.CreateConnection();
            var sqlCommand = new GetPropertyBrokenRulesSqlCommand();
            results = (await conn.QueryAsync<FloodPropertyBrokenRuleEntity>(sqlCommand.ToString(),
                        commandType: CommandType.Text,
                        commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                        param: new { @p_ApplicationId = applicationId , @p_PamsPin = pamsPin })).ToList();

            return results ?? new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brokenRule"></param>
        /// <returns></returns>
        public async Task SavePropertyBrokenRule(FloodPropertyBrokenRuleEntity brokenRule)
        {
            using var conn = context.CreateConnection();
            var sqlCommand = new SavePropertyBrokenRulesSqlCommand();
            await conn.ExecuteAsync(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_ApplicationId = brokenRule.ApplicationId,
                    @p_PamsPin = brokenRule.PamsPin,
                    @p_SectionId = brokenRule.SectionId,
                    @p_Message = brokenRule.Message,
                    @p_IsPropertyFlow = brokenRule.IsPropertyFlow
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brokenRules"></param>
        /// <returns></returns>
        public async Task SavePropertyBrokenRules(List<FloodPropertyBrokenRuleEntity> brokenRules)
        {
            using var conn = context.CreateConnection();
            var sqlCommand = new SavePropertyBrokenRulesSqlCommand();

            foreach (var brokenRule in brokenRules)
            {
                await conn.ExecuteAsync(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = brokenRule.ApplicationId,
                        @p_PamsPin = brokenRule.PamsPin,
                        @p_SectionId = brokenRule.SectionId,
                        @p_Message = brokenRule.Message,
                        @p_IsPropertyFlow = brokenRule.IsPropertyFlow
                    });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public async Task DeletePropertyBrokenRulesAsync(int applicationId, PropertySectionEnum section , string pamsPin)

        {
            using var conn = context.CreateConnection();
            var sqlCommand = new DeletePropertyBrokenRulesSqlCommand();
            await conn.ExecuteAsync(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_SectionId = (int)section,
                    @p_ApplicationId = applicationId,
                    @p_PamsPin = pamsPin
                });
        }
    }
}
